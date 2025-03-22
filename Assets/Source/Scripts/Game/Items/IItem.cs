using UnityEngine;

namespace Source.Scripts.Game.Items
{
    public interface IItem
    {
        string ItemName { get; }
        Sprite Icon { get; }
        GameObject Prefab { get; }

        public void OnPickup();
        public void OnDrop();
    }
}