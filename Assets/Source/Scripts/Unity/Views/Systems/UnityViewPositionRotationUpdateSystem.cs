using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityViewPositionRotationUpdateSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter positionFilter = world.Filter<UnityViewComponent>().Inc<PositionComponent>().End();
            EcsFilter rotationFilter = world.Filter<UnityViewComponent>().Inc<RotationComponent>().End();
            
            EcsPool<UnityViewComponent> unityViewPool = world.GetPool<UnityViewComponent>();
            EcsPool<PositionComponent> positionPool = world.GetPool<PositionComponent>();
            EcsPool<RotationComponent> rotationPool = world.GetPool<RotationComponent>();

            foreach (int entity in positionFilter)
            {
                ref UnityViewComponent unityViewComponent = ref unityViewPool.Get(entity);
                ref PositionComponent positionComponent = ref positionPool.Get(entity);

                Transform unityViewTransform = unityViewComponent.Value.transform;
                unityViewTransform.position = new Vector3(positionComponent.Value.X, positionComponent.Value.Y, positionComponent.Value.Z);
            }
        
            foreach (int entity in rotationFilter)
            {
                ref UnityViewComponent unityViewComponent = ref unityViewPool.Get(entity);
                ref RotationComponent rotationComponent = ref rotationPool.Get(entity);

                Transform unityViewTransform = unityViewComponent.Value.transform;
                unityViewTransform.eulerAngles = new Vector3(rotationComponent.Value.X, rotationComponent.Value.Y, rotationComponent.Value.Z);
            }
        }
    }
}