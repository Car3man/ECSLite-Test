using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityIdComponentConverter : UnityComponentConverter<IdComponent>
    {
        [SerializeField] private string id;
        
        protected override void SetupComponent(EcsWorld world, ref IdComponent component)
        {
            component.Id = id;
        }
    }
}