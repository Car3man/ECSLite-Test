using System.Numerics;

namespace ECSTest.Core
{
    public interface INavigationService
    {
        public Vector3[] CalculatePath(Vector3 source, Vector3 destination);
    }
}