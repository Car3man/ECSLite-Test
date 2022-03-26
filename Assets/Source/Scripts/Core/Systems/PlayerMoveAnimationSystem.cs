using System.Numerics;
using Leopotam.EcsLite;

namespace ECSTest.Core
{
    public class PlayerMoveAnimationSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            ProcessCleanUp(systems);
            ProcessExecute(systems);
        }

        private void ProcessCleanUp(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world
                .Filter<PlayerComponent>()
                .Inc<MoveAnimatorComponent>()
                .Inc<PositionComponent>()
                .Inc<SetAnimationPropertyComponent>()
                .End();
            
            EcsPool<SetAnimationPropertyComponent> setAnimationPropertyPool = world.GetPool<SetAnimationPropertyComponent>();

            foreach (int entity in filter)
            {
                setAnimationPropertyPool.Del(entity);
            }
        }

        private void ProcessExecute(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<PlayerComponent>().Inc<MoveAnimatorComponent>().Inc<PositionComponent>().End();

            EcsPool<MoveAnimatorComponent> moveAnimatorPool = world.GetPool<MoveAnimatorComponent>();
            EcsPool<PositionComponent> positionPool = world.GetPool<PositionComponent>();
            EcsPool<SetAnimationPropertyComponent> setAnimationPropertyPool = world.GetPool<SetAnimationPropertyComponent>();

            foreach (int entity in filter)
            {
                ref MoveAnimatorComponent moveAnimatorComponent = ref moveAnimatorPool.Get(entity);
                PositionComponent positionComponent = positionPool.Get(entity);

                float moveDelta = Vector3.Distance(moveAnimatorComponent.LastPosition, positionComponent.Value);
                moveAnimatorComponent.LastPosition = positionComponent.Value;
                
                ref SetAnimationPropertyComponent setAnimationPropertyComponent = ref setAnimationPropertyPool.Add(entity);
                setAnimationPropertyComponent.Name = "MoveDelta";
                setAnimationPropertyComponent.Value = moveDelta;
            }
        }
    }
}
