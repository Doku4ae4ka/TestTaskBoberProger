using Source.Scripts.Game.Inventory.Items;

namespace Source.Scripts.Infrastructure.Services.SignalService
{
    public static class Signals
    {
        #region UI
        
        public struct OnPickupHintStatusChanged
        {
            public bool IsShown;
            public IItem Item;

            public OnPickupHintStatusChanged(bool isShown, IItem item = null)
            {
                IsShown = isShown;
                Item = item;
            }
        }
        
        public struct OnSlotChanged
        {
            public int ItemIndex;
            public IItem Item;

            public OnSlotChanged(int itemIndex, IItem item)
            {
                ItemIndex = itemIndex;
                Item = item;
            }
        }
        
        public struct OnItemAdded
        {
            public int ItemIndex;
            public IItem Item;

            public OnItemAdded(int itemIndex, IItem item)
            {
                ItemIndex = itemIndex;
                Item = item;
            }
        }
        
        public struct OnItemRemoved
        {
            public int ItemIndex;

            public OnItemRemoved(int itemIndex)
            {
                ItemIndex = itemIndex;
            }
        }
        
        #endregion
    }
}