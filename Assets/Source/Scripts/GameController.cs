using ECSTest.Core;
using ECSTest.Unity.Views;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace ECSTest
{
    public class GameController : MonoBehaviour
    {
        [Inject] private IInputService _inputService;
        [Inject] private INavigationService _navigationService;
        [Inject] private IPhysicsService _physicsService;
        [Inject] private ITimeService _timeService;
        
        private EcsWorld _world;
        private EcsSystems _coreSystems;
        private EcsSystems _viewSystems;

        private void Start () 
        {
            _world = new EcsWorld ();
        
            // Core
            _coreSystems = new EcsSystems(_world)
                .Add(new LinkSystem())
                .Add(new NavigationSystem(_navigationService, _timeService))
                .Add(new PlayerMoveSystem(_inputService, _physicsService))
                .Add(new PlayerMoveAnimationSystem())
                .Add(new GateOpenSystem());
            _coreSystems.Init ();
        
            // Unity Views
            _viewSystems = new EcsSystems(_world)
                .Add(new UnityViewConverterSystem())
                .Add(new UnityViewPositionRotationUpdateSystem())
                .Add(new UnityViewAnimationSystem());
            _viewSystems.Init();
        }
    
        private void Update () 
        {
            _coreSystems?.Run ();
            _viewSystems?.Run();
        }

        private void OnDestroy () 
        {
            _coreSystems?.Destroy();
            _viewSystems?.Destroy();
            _world?.Destroy();
        }
    }
}