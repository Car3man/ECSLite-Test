using ECSTest.Core;
using ECSTest.Unity.Views;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace ECSTest.Unity.Services
{
    public class UnityPhysicsService : IPhysicsService
    {
        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        public bool RaycastScreenToWorld(Vector2 screenPoint, out PhysicsRaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(new UnityEngine.Vector3(screenPoint.X, screenPoint.Y));

            int countHits = Physics.RaycastNonAlloc(ray, _raycastHits, float.MaxValue);
            if (countHits > 0)
            {
                Vector3 point = new Vector3(_raycastHits[0].point.x, _raycastHits[0].point.y, _raycastHits[0].point.z);
                hit = new PhysicsRaycastHit(point);
                return true;
            }

            hit = default;
            return false;
        }
    }
}