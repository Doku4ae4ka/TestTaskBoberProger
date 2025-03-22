using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class InventoryHud : MonoBehaviour
    {
        [SerializeField] private List<InventoryItemHud> hudItems;
        private ISignalSubscriber _subscriber;

        private void Awake()
        {
            _subscriber = AllServices.Container.Single<ISignalSubscriber>();
        }
        private void Start()
        {
            _subscriber.Subscribe<Signals.OnItemAdded>(OnItemAdded);
            _subscriber.Subscribe<Signals.OnItemRemoved>(OnItemRemoved);
            _subscriber.Subscribe<Signals.OnSlotChanged>(OnSlotChanged);
        }

        private void OnItemAdded(Signals.OnItemAdded data)
        {
            hudItems[data.ItemIndex].ShowItem(data.Item);
        }

        private void OnItemRemoved(Signals.OnItemRemoved data)
        {
            hudItems[data.ItemIndex].HideItem();
        }

        private void OnSlotChanged(Signals.OnSlotChanged data)
        {
            foreach (var item in hudItems) 
                item.DeselectItemV2();
            hudItems[data.ItemIndex].SelectItemV2();
        }
        
        private void OnDisable()
        {
            _subscriber.Unsubscribe<Signals.OnItemAdded>(OnItemAdded);
            _subscriber.Unsubscribe<Signals.OnItemRemoved>(OnItemRemoved);
            _subscriber.Unsubscribe<Signals.OnSlotChanged>(OnSlotChanged);
        }
    }
}