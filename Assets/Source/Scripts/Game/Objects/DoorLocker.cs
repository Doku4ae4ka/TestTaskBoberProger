using System;
using Source.Scripts.Game.Items;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using UnityEngine;

namespace Source.Scripts.Game.Objects
{
    public class DoorLocker : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool doorLocked;
        [SerializeField] private int keyId;
        [SerializeField] private ItemBase keyData;
        [SerializeField] private Outline outline;
        private Inventory.Inventory _characterInventory;
        private ISignalRegister _signalRegister;
        private ISignalSubscriber _signalSubscriber;

        public bool DoorLocked => doorLocked;
        public int KeyId => keyId;

        private void Awake()
        {
            _signalRegister = AllServices.Container.Single<ISignalRegister>();
            _signalSubscriber = AllServices.Container.Single<ISignalSubscriber>();
        }

        private void Start()
        {
            _signalSubscriber.Subscribe<Signals.OnInventoryInit>(SetCharacterInventory);
            _signalSubscriber.Subscribe<Signals.OnDoorHintStatusChanged>(ChangeOutlineStatus);
        }

        public IItem GetItem() => keyData;

        public void OnInteract()
        {
            if (doorLocked)
            {
                if (_characterInventory.TryGetWorldItem(_characterInventory.SelectedSlotIndex, out var item))
                {
                    var keyItem = item.GetItem() as KeyItem;
                    if (keyItem && keyItem.keyId == keyId)
                        doorLocked = false;
                }   
            }
            else
                _signalRegister.RegistryRaise(new Signals.OnDoorOpened());
        }

        private void SetCharacterInventory(Signals.OnInventoryInit data) =>
            _characterInventory = data.Inventory;
        
        private void ChangeOutlineStatus(Signals.OnDoorHintStatusChanged data)
        {
            if (data.IsShown && data.Item != null)
            {
                var keyItem = data.Item as KeyItem;
                if (keyItem && keyItem.keyId == data.Locker.KeyId && doorLocked)
                    outline.OutlineColor = Color.green;
                else if (!(keyItem && keyItem.keyId == data.Locker.KeyId) && doorLocked)
                    outline.OutlineColor = Color.red;
                else 
                    outline.OutlineColor = Color.green;
            }
            if (data.IsShown && data.Item == null && data.Locker.DoorLocked)
                outline.OutlineColor = Color.red;
            outline.enabled = data.IsShown;
        }
        
        private void OnDisable()
        {
            _signalSubscriber.Unsubscribe<Signals.OnInventoryInit>(SetCharacterInventory);
            _signalSubscriber.Unsubscribe<Signals.OnDoorHintStatusChanged>(ChangeOutlineStatus);
        }
    }
}