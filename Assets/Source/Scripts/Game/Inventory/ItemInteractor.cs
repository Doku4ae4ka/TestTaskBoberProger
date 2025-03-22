using Source.Scripts.Game.Inventory.Items;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using Source.Scripts.Services.Input;
using UnityEngine;

namespace Source.Scripts.Game.Inventory
{
    public class ItemInteractor : MonoBehaviour
    {
        private const string Interaction = "Interaction";
        private const string Pickupable = "Pickupable";

        [Header("Зависимости")]
        [SerializeField] private Inventory inventory;

        [Header("Настройки подбора предметов")]
        [SerializeField] private float pickupRange = 5f;
        [SerializeField] private float dropForce = 5f;
        
        private ISignalRegister _signalRegister;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _signalRegister = AllServices.Container.Single<ISignalRegister>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            HandlePickup();

            HandleThrow();
            HandleDrop();
        }

        private void HandlePickup()
        {
            //Оптимизации НОЛЬ
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
            if (Physics.Raycast(ray, out var hit, pickupRange, LayerMask.GetMask(Pickupable)))
            {
                if(!hit.collider.transform.root.TryGetComponent<WorldItem>(out var worldItem)) return;
                _signalRegister.RegistryRaise(
                    new Signals.OnPickupHintStatusChanged(true, worldItem.GetItem()));
                
                if (!_inputService.IsInteractButtonUp()) return;

                inventory.TryAddItem(worldItem);
            }
            else _signalRegister.RegistryRaise(
                new Signals.OnPickupHintStatusChanged(false));
        }

        private void HandleThrow()
        {
            if (!_inputService.IsThrowButtonUp()) return;
            
            if (inventory.TryGetWorldItem(inventory.SelectedSlotIndex, out var worldItem))
                worldItem.OnDrop(transform, dropForce);
            
            inventory.RemoveItem(inventory.SelectedSlotIndex);
        }
        private void HandleDrop()
        {
            if (!_inputService.IsThrowButtonUp()) return;
            
            if (inventory.TryGetWorldItem(inventory.SelectedSlotIndex, out var worldItem))
                worldItem.OnDrop(transform, 0.1f);
            
            inventory.RemoveItem(inventory.SelectedSlotIndex);
        }
        
    }
}