using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Source.Scripts.Game.Inventory.Items
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