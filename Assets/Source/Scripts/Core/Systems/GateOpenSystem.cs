using Leopotam.EcsLite;
using Vector3 = System.Numerics.Vector3;

namespace ECSTest.Core
{
    public class GateOpenSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter playerFilter = world.Filter<PlayerComponent>().Inc<PositionComponent>().End();
            EcsFilter gateButtonFilter = world.Filter<GateButtonComponent>().Inc<LinkComponent>().Inc<PositionComponent>().End();
            
            EcsPool<GateComponent> gatePool = world.GetPool<GateComponent>();
            EcsPool<GateButtonComponent> gateButtonPool = world.GetPool<GateButtonComponent>();
            EcsPool<PositionComponent> positionPool = world.GetPool<PositionComponent>();
            EcsPool<LinkComponent> linkPool = world.GetPool<LinkComponent>();
            EcsPool<PlayAnimationComponent> playAnimationPool = world.GetPool<PlayAnimationComponent>();

            foreach (int playerEntity in playerFilter)
            {
                PositionComponent playerPositionComponent = positionPool.Get(playerEntity);
                
                foreach (int gateButtonEntity in gateButtonFilter)
                {
                    ref GateButtonComponent gateButtonComponent = ref gateButtonPool.Get(gateButtonEntity);
                    
                    if (gateButtonComponent.Pressed)
                    {
                        continue;
                    }
                    
                    PositionComponent gatePositionComponent = positionPool.Get(gateButtonEntity);
                    
                    if (Vector3.Distance(playerPositionComponent.Value, gatePositionComponent.Value) < 1f)
                    {
                        LinkComponent gateButtonLinkComponent = linkPool.Get(gateButtonEntity);
                        
                        if (gateButtonLinkComponent.Value.Unpack(world, out int gateEntity))
                        {
                            ref GateComponent gateComponent = ref gatePool.Get(gateEntity);
                            gateComponent.Opened = true;
                            playAnimationPool.Add(gateEntity).Name = "Open";
                        }

                        playAnimationPool.Add(gateButtonEntity).Name = "Press";
                        gateButtonComponent.Pressed = true;
                    }
                }
            }
        }
    }
}