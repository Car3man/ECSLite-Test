using System.Numerics;

namespace ECSTest.Core
{
    public interface IPhysicsService
    {
        bool RaycastScreenToWorld(Vector2 screenPoint, out PhysicsRaycastHit hit);
    }

    public struct PhysicsRaycastHit
    {
        public Vector3 Point;

        public PhysicsRaycastHit(Vector3 point)
        {
            Point = point;
        }
    }
}