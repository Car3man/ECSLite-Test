using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityViewAnimationSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            ProcessPlayAnimation(systems);
            ProcessSetAnimationProperty(systems);
        }

        private void ProcessPlayAnimation(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<UnityViewComponent>().Inc<PlayAnimationComponent>().End();
            
            EcsPool<UnityViewComponent> unityViewPool = world.GetPool<UnityViewComponent>();
            EcsPool<PlayAnimationComponent> playAnimationPool = world.GetPool<PlayAnimationComponent>();

            foreach (int entity in filter)
            {
                ref UnityViewComponent unityViewComponent = ref unityViewPool.Get(entity);
                ref PlayAnimationComponent playAnimationComponent = ref playAnimationPool.Get(entity);
                unityViewComponent.Value.GetComponent<Animator>().Play(playAnimationComponent.Name);
                playAnimationPool.Del(entity);
            }
        }

        private void ProcessSetAnimationProperty(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<UnityViewComponent>().Inc<SetAnimationPropertyComponent>().End();
            
            EcsPool<UnityViewComponent> unityViewPool = world.GetPool<UnityViewComponent>();
            EcsPool<SetAnimationPropertyComponent> setAnimationPropertyPool = world.GetPool<SetAnimationPropertyComponent>();

            foreach (int entity in filter)
            {
                ref UnityViewComponent unityViewComponent = ref unityViewPool.Get(entity);
                ref SetAnimationPropertyComponent setAnimationPropertyComponent = ref setAnimationPropertyPool.Get(entity);

                switch (setAnimationPropertyComponent.Value)
                {
                    case bool boolValue:
                        unityViewComponent.Value.GetComponent<Animator>().SetBool(setAnimationPropertyComponent.Name, boolValue);
                        break;
                    case float floatValue:
                        unityViewComponent.Value.GetComponent<Animator>().SetFloat(setAnimationPropertyComponent.Name, floatValue);
                        break;
                    case int intValue:
                        unityViewComponent.Value.GetComponent<Animator>().SetInteger(setAnimationPropertyComponent.Name, intValue);
                        break;
                }
                
                setAnimationPropertyPool.Del(entity);
            }
        }
    }
}