using System.Numerics;

namespace ECSTest.Core
{
    public interface IInputService
    {
        Vector2 ScreenPoint { get; }
        bool IsLeftMouseButtonDown();
    }
}