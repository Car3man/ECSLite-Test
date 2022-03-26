using Leopotam.EcsLite;
using Vector2 = System.Numerics.Vector2;

namespace ECSTest.Core
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private IInputService _inputService;
        private IPhysicsService _physicsService;
        
        public PlayerMoveSystem(IInputService inputService, IPhysicsService physicsService)
        {
            _inputService = inputService;
            _physicsService = physicsService;
        }

        public void Run(EcsSystems systems)
        {
            if (_inputService.IsLeftMouseButtonDown())
            {
                Vector2 screenPoint = _inputService.ScreenPoint;
                
                if (_physicsService.RaycastScreenToWorld(screenPoint, out PhysicsRaycastHit hit))
                {
                    ProcessCleanUp(systems);
                    ProcessExecute(systems, hit);
                }
            }
        }

        private void ProcessCleanUp(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<PlayerComponent>().Inc<NavigationDestinationComponent>().End();
                    
            EcsPool<NavigationDestinationComponent> navigationDestinationPool = world.GetPool<NavigationDestinationComponent>();
                    
            foreach (int entity in filter)
            {
                navigationDestinationPool.Del(entity);
            }
        }

        private void ProcessExecute(EcsSystems systems, PhysicsRaycastHit hit)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world.Filter<PlayerComponent>().End();
                    
            EcsPool<NavigationDestinationComponent> navigationDestinationPool = world.GetPool<NavigationDestinationComponent>();
                    
            foreach (int entity in filter)
            {
                navigationDestinationPool.Add(entity).Value = hit.Point;
            }
        }
    }
}