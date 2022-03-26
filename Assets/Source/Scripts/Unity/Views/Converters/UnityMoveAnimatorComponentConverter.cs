using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityMoveAnimatorComponentConverter : UnityComponentConverter<MoveAnimatorComponent>
    {
        protected override void SetupComponent(EcsWorld world, ref MoveAnimatorComponent component)
        {
            Vector3 position = transform.position;
            component.LastPosition = new System.Numerics.Vector3(position.x, position.y, position.z);
        }
    }
}