using ECSTest.Core;
using Leopotam.EcsLite;

namespace ECSTest.Unity.Views
{
    public class UnityPlayerComponentConverter : UnityComponentConverter<PlayerComponent>
    {
        protected override void SetupComponent(EcsWorld world, ref PlayerComponent component)
        {

        }
    }
}