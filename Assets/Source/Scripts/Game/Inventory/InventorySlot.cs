using System;
using Source.Scripts.Game.Items;

namespace Source.Scripts.Game.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public IItem Item;
    }
}