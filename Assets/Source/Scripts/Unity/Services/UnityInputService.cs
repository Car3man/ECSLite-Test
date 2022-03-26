using ECSTest.Core;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ECSTest.Unity.Services
{
    public class UnityInputService : IInputService
    {
        public Vector2 ScreenPoint => new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}