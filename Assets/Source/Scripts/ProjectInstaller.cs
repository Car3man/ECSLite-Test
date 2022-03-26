using ECSTest.Core;
using ECSTest.Unity.Services;
using Zenject;

namespace ECSTest
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<IInputService>().To<UnityInputService>().AsSingle();
            Container.Bind<IPhysicsService>().To<UnityPhysicsService>().AsSingle();
            Container.Bind<INavigationService>().To<UnityNavigationService>().AsSingle();
        }
    }
}