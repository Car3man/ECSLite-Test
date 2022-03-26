using ECSTest.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityRotationComponentConverter : UnityComponentConverter<RotationComponent>
    {
        protected override void SetupComponent(EcsWorld world, ref RotationComponent component)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            component.Value = new System.Numerics.Vector3(rotation.x, rotation.y, rotation.z);
        }
    }
}
