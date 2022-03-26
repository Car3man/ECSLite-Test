using System;
using Leopotam.EcsLite;
using Vector3 = System.Numerics.Vector3;

namespace ECSTest.Core
{
    public class NavigationSystem : IEcsRunSystem
    {
        private readonly INavigationService _navigationService;
        private readonly ITimeService _timeService;

        public NavigationSystem(INavigationService navigationService, ITimeService timeService)
        {
            _navigationService = navigationService;
            _timeService = timeService;
        }

        public void Run(EcsSystems systems)
        {
            ProcessCleanUp(systems);
            ProcessStartExecute(systems);
            ProcessExecute(systems);
        }

        private void ProcessCleanUp(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world
                .Filter<NavigationAgentComponent>()
                .Inc<PositionComponent>()
                .Inc<NavigationDestinationComponent>()
                .Inc<NavigationMovePathComponent>()
                .End();
            
            EcsPool<NavigationMovePathComponent> navigationMovePointPool = world.GetPool<NavigationMovePathComponent>();

            foreach (int entity in filter)
            {
                navigationMovePointPool.Del(entity);
            }
        }

        private void ProcessStartExecute(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world
                .Filter<NavigationAgentComponent>()
                .Inc<PositionComponent>()
                .Inc<NavigationDestinationComponent>()
                .End();
            EcsFilter cleanUpFilter = world
                .Filter<NavigationAgentComponent>()
                .Inc<PositionComponent>()
                .Inc<NavigationDestinationComponent>()
                .Inc<NavigationMovePathComponent>()
                .End();
            
            EcsPool<PositionComponent> positionPool = world.GetPool<PositionComponent>();
            EcsPool<NavigationDestinationComponent> navigationDestinationPool = world.GetPool<NavigationDestinationComponent>();
            EcsPool<NavigationMovePathComponent> navigationMovePathPool = world.GetPool<NavigationMovePathComponent>();

            foreach (int entity in cleanUpFilter)
            {
                navigationDestinationPool.Del(entity);
            }
            
            foreach (int entity in filter)
            {
                ref PositionComponent positionComponent = ref positionPool.Get(entity);
                NavigationDestinationComponent navigationDestinationComponent = navigationDestinationPool.Get(entity);

                Vector3[] path = _navigationService.CalculatePath(positionComponent.Value, navigationDestinationComponent.Value);
                navigationMovePathPool.Add(entity).Path = path;
                navigationDestinationPool.Del(entity);
            }
        }

        private void ProcessExecute(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsFilter filter = world
                .Filter<NavigationAgentComponent>()
                .Inc<PositionComponent>()
                .Inc<RotationComponent>()
                .Inc<NavigationMovePathComponent>()
                .End();
            
            EcsPool<PositionComponent> positionPool = world.GetPool<PositionComponent>();
            EcsPool<RotationComponent> rotationPool = world.GetPool<RotationComponent>();
            EcsPool<NavigationMovePathComponent> navigationMovePathPool = world.GetPool<NavigationMovePathComponent>();
            EcsPool<NavigationAgentComponent> navigationAgentPool = world.GetPool<NavigationAgentComponent>();

            foreach (int entity in filter)
            {
                ref PositionComponent positionComponent = ref positionPool.Get(entity);
                ref RotationComponent rotationComponent = ref rotationPool.Get(entity);
                ref NavigationMovePathComponent navigationMovePathComponent = ref navigationMovePathPool.Get(entity);
                NavigationAgentComponent navigationAgentComponent = navigationAgentPool.Get(entity);

                if (navigationMovePathComponent.Path.Length == 0 ||
                    navigationMovePathComponent.Index >= navigationMovePathComponent.Path.Length)
                {
                    navigationMovePathPool.Del(entity);
                    continue;
                }

                int moveToIndex = navigationMovePathComponent.Index;
                Vector3 moveToPoint = navigationMovePathComponent.Path[moveToIndex];
                Vector3 goalPoint = navigationMovePathComponent.Path[navigationMovePathComponent.Path.Length - 1];

                float moveSpeed = navigationAgentComponent.MoveSpeed;
                
                rotationComponent.Value.Y = (float)(Red2Deg() * Math.Atan2(moveToPoint.X - positionComponent.Value.X, moveToPoint.Z - positionComponent.Value.Z));
                positionComponent.Value = MoveTowards(positionComponent.Value, moveToPoint, moveSpeed * _timeService.DeltaTime);
                
                if (Vector3.Distance(positionComponent.Value, moveToPoint) < 0.1f)
                {
                    navigationMovePathComponent.Index++;
                }
                
                if (Vector3.Distance(positionComponent.Value, goalPoint) < 0.01f)
                {
                    navigationMovePathPool.Del(entity);
                }
            }
        }
        
        private float Red2Deg()
        {
            return 360f / (float)(Math.PI * 2);
        }
        
        private Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 a = target - current;
            float magnitude = Magnitude(a);
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        private float Magnitude(Vector3 vector)
        {
            return (float) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }
    }
}