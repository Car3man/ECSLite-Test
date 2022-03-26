using ECSTest.Core;
using UnityEngine;

namespace ECSTest.Unity.Services
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}