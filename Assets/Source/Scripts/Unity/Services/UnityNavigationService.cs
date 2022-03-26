using ECSTest.Core;
using UnityEngine.AI;
using Vector3 = System.Numerics.Vector3;

namespace ECSTest.Unity.Services
{
    public class UnityNavigationService : INavigationService
    {
        public Vector3[] CalculatePath(Vector3 source, Vector3 destination)
        {
            UnityEngine.Vector3 unitySource = new UnityEngine.Vector3(source.X, source.Y, source.Z);
            UnityEngine.Vector3 unityDestination = new UnityEngine.Vector3(destination.X, destination.Y, destination.Z);
                
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(unitySource, unityDestination, NavMesh.AllAreas, path);
            
            Vector3[] points = new Vector3[path.corners.Length];
            for (int i = 0; i < path.corners.Length; i++)
            {
                points[i] = new Vector3(path.corners[i].x, path.corners[i].y, path.corners[i].z);
            }
            return points;
        }
    }
}