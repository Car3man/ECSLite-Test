using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityLinkRequestComponentConverter : UnityComponentConverter<LinkRequestComponent>
    {
        [SerializeField] private string otherId;
        
        protected override void SetupComponent(EcsWorld world, ref LinkRequestComponent component)
        {
            component.OtherId = otherId;
        }
    }
}