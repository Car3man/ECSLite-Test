using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityNavigationAgentComponentConverter : UnityComponentConverter<NavigationAgentComponent>
    {
        [SerializeField] private float moveSpeed;
        
        protected override void SetupComponent(EcsWorld world, ref NavigationAgentComponent component)
        {
            component.MoveSpeed = moveSpeed;
        }
    }
}