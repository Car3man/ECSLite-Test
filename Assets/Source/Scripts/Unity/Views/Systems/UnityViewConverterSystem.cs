using System.Numerics;
using ECSTest.Core;
using ECSTest.Unity.Services;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;
using Vector3 = System.Numerics.Vector3;

namespace ECSTest.Unity.Views
{
    public class UnityViewConverterSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            
            ConvertUnityViews();
        }

        private void ConvertUnityViews()
        {
            UnityView[] unityViews = Object.FindObjectsOfType<UnityView>();
            foreach (UnityView unityView in unityViews)
            {
                if (unityView.Linked)
                {
                    continue;
                }
            
                CreateUnityView(unityView);
            }
        }

        private void CreateUnityView(UnityView unityView)
        {
            int newEntity = _world.NewEntity();
            unityView.LinkToEntity(_world, newEntity);

            ConvertBaseComponents(newEntity, unityView);
            ConvertCustomComponents(newEntity, unityView);
        }

        private void ConvertBaseComponents(int entity, UnityView unityView)
        {
            EcsPool<UnityViewComponent> unityViewPool = _world.GetPool<UnityViewComponent>();
            unityViewPool.Add(entity).Value = unityView;

            EcsPool<PositionComponent> positionPool = _world.GetPool<PositionComponent>();
            UnityPositionComponentConverter positionComponentConverter = unityView.GetComponent<UnityPositionComponentConverter>();
            if (positionComponentConverter)
            {
                positionComponentConverter.Convert(entity, _world);
            }
            else
            {
                UnityEngine.Vector3 unityPosition = unityView.transform.position;
                Vector3 position = new Vector3(unityPosition.x, unityPosition.y, unityPosition.z);
                positionPool.Add(entity).Value = position;
            }
            
            EcsPool<RotationComponent> rotationPool = _world.GetPool<RotationComponent>();
            UnityRotationComponentConverter rotationComponentConverter = unityView.GetComponent<UnityRotationComponentConverter>();
            if (rotationComponentConverter)
            {
                rotationComponentConverter.Convert(entity, _world);
            }
            else
            {
                UnityEngine.Vector3 unityRotation = unityView.transform.eulerAngles;
                Vector3 rotation = new Vector3(unityRotation.x, unityRotation.y, unityRotation.z);
                rotationPool.Add(entity).Value = rotation;
            }
        }

        private void ConvertCustomComponents(int entity, UnityView unityView)
        {
            IUnityComponentConverter[] converters = unityView.GetComponents<IUnityComponentConverter>();
            foreach (IUnityComponentConverter converter in converters)
            {
                converter.Convert(entity, _world);
            }
        }
    }
}