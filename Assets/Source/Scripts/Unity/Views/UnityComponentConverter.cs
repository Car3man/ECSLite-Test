using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    [RequireComponent(typeof(UnityView)), DisallowMultipleComponent]
    public abstract class UnityComponentConverter<T> : MonoBehaviour, IUnityComponentConverter where T : struct
    {
        private bool _converter;

        public void Convert(int entity, EcsWorld world)
        {
            if (_converter)
            {
                Debug.LogWarning("You are trying convert already converted unity mono component");
                return;
            }
        
            _converter = true;
            
            ref T component = ref AddComponent(entity, world);
            SetupComponent(world, ref component);
            DestroyImmediate(this);
        }

        protected abstract void SetupComponent(EcsWorld world, ref T component);

        private ref T AddComponent(int entity, EcsWorld world)
        {
            EcsPool<T> pool = world.GetPool<T>();
            return ref pool.Add(entity);
        }
    }
}