using UnityEngine;

namespace Source.Scripts.Game.Items
{
    public interface ICarried : IWorldObject
    {
        public void OnDrop(Transform thrower, float force = 1f);
        
        public void OnPickup();
    }
    
    public interface IInteractable : IWorldObject
    {
        public void OnInteract();
        
    }

    public interface IWorldObject
    {
        public IItem GetItem();
        
    }
}