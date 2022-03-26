using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Unity.Views
{
    public class UnityView : MonoBehaviour
    {
        public bool Linked { get; private set; }
        public EcsPackedEntityWithWorld Entity { get; private set; }

        public void LinkToEntity(EcsWorld world, int entity)
        {
            if (Linked)
            {
                Debug.LogWarning("You are trying link already linked unity view");
                return;
            }
        
            Linked = true;
            Entity = world.PackEntityWithWorld(entity);
            
            if (string.IsNullOrEmpty(gameObject.name) || gameObject.name == "New Game Object")
            {
                gameObject.name = $"Entity {entity}";
            }
            else
            {
                gameObject.name = $"{gameObject.name} - Entity {entity}";
            }
        }

        public void Unlink()
        {
            if (!Linked)
            {
                Debug.LogWarning("You are trying unlink not linked unity view");
                return;
            }

            Linked = false;
            Entity = default;
        }
    }
}