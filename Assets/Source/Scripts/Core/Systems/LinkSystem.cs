using Leopotam.EcsLite;

namespace ECSTest.Core
{
    public class LinkSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter currentFilter = world.Filter<LinkRequestComponent>().End();
            EcsFilter otherFilter = world.Filter<IdComponent>().End();

            EcsPool<LinkRequestComponent> linkRequestPool = world.GetPool<LinkRequestComponent>();
            EcsPool<LinkComponent> linkPool = world.GetPool<LinkComponent>();
            EcsPool<IdComponent> idPool = world.GetPool<IdComponent>();
            
            foreach (int currentEntity in currentFilter)
            {
                LinkRequestComponent linkRequestComponent = linkRequestPool.Get(currentEntity);
                
                foreach (int otherEntity in otherFilter)
                {
                    IdComponent idComponent = idPool.Get(otherEntity);

                    if (linkRequestComponent.OtherId == idComponent.Id)
                    {
                        linkPool.Add(currentEntity).Value = world.PackEntity(otherEntity);
                    }
                }
                
                linkRequestPool.Del(currentEntity);
            }
        }
    }
}