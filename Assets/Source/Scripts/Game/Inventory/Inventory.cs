using System.Collections.Generic;
using Source.Scripts.Game.Inventory.Items;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using Source.Scripts.Services.Input;
using UnityEngine;

namespace Source.Scripts.Game.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [Header("Зависимости")]
        [SerializeField] private HandsDisplay handsDisplay;
        
        [Header("Настройки инвентаря")]
        [SerializeField] private int slotCount = 5;
        [SerializeField] private Transform dropItemSpawnPosition;
        private InventorySlot[] _slots;
        private Dictionary<IItem, WorldItem> _worldItems = new();

        public int SelectedSlotIndex { get; private set; } = 0;
        
        private ISignalRegister _signalRegister;
        private IInputService _inputService;

        private void Awake()
        {
            _signalRegister = AllServices.Container.Single<ISignalRegister>();
            _inputService = AllServices.Container.Single<IInputService>();
            _slots = new InventorySlot[slotCount];
            for (var i = 0; i < slotCount; i++)
                _slots[i] = new InventorySlot();
            
        }

        private void Update() => 
            HandleSlotSelection();

        public bool TryGetWorldItem(int slotIndex, out WorldItem item)
        {
            if (_slots[slotIndex].Item == null)
            {
                item = null;
                return false;
            }
            
            if (_worldItems.TryGetValue(_slots[slotIndex].Item, out var worldItem))
            {
                item = worldItem;
                return true;
            }
            item = null;
            return false;
        }
        
        public bool TryAddItem(WorldItem worldItem)
        {
            for (var index = 0; index < slotCount; index++)
            {
                var itemInfo = worldItem.GetItem();
                
                if (_slots[index].Item != null) continue;
                
                _worldItems.TryAdd(itemInfo, worldItem);
                worldItem.OnPickup();
                
                _slots[index].Item = itemInfo;
                if(SelectedSlotIndex == index) handsDisplay.DisplayItem(index);
                _signalRegister.RegistryRaise(new Signals.OnItemAdded(index, itemInfo));
#if DEBUG
                Debug.Log($"Предмет {itemInfo.ItemName} добавлен в слот {index}");
#endif
                return true;
            }
#if DEBUG
            Debug.Log("Инвентарь полон!");
#endif
            return false;
        }
        
        public void RemoveItem(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < slotCount && _slots[slotIndex].Item != null)
            {
                var item = _slots[slotIndex].Item;
                _worldItems.Remove(_slots[slotIndex].Item);
                _slots[slotIndex].Item = null;
                _signalRegister.RegistryRaise(new Signals.OnItemRemoved(slotIndex));
#if DEBUG
                Debug.Log($"Предмет {item.ItemName} удалён из слота {slotIndex}");
#endif
            }
        }
        
        public void SelectSlot(int index)
        {
            if (index >= 0 && index < slotCount)
            {
                handsDisplay.DisplayItem(index);
                SelectedSlotIndex = index;
                _signalRegister.RegistryRaise(new Signals.OnSlotChanged(index, _slots[index].Item));
#if DEBUG
                Debug.Log($"Выбран слот {index}");
#endif
            }
        }
        
        private void HandleSlotSelection()
        {
            var index = _inputService.IsSlotSelected();
            if (index != -1) SelectSlot(index);
            
            var scroll = _inputService.ScrollMagnitude;
            switch (scroll)
            {
                case > 0f:
                    SelectSlot((SelectedSlotIndex + 1) % slotCount);
                    break;
                case < 0f:
                    SelectSlot((SelectedSlotIndex - 1 + slotCount) % slotCount);
                    break;
            }
        }
    }
}