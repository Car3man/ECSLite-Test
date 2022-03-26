using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityPositionComponentConverter : UnityComponentConverter<PositionComponent>
    {
        protected override void SetupComponent(EcsWorld world, ref PositionComponent component)
        {
            Vector3 position = transform.position;
            component.Value = new System.Numerics.Vector3(position.x, position.y, position.z);
        }
    }
}
