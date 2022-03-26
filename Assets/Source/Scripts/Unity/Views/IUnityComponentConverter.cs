using Leopotam.EcsLite;

namespace ECSTest.Unity.Views
{
    public interface IUnityComponentConverter
    {
        void Convert(int entity, EcsWorld world);
    }
}