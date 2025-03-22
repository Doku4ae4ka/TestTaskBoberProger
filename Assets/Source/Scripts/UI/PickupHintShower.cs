using Source.Scripts.Game.Inventory.Items;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class PickupHintShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text pickupHintText;
        private ISignalSubscriber _subscriber;

        private void Awake()
        {
            _subscriber = AllServices.Container.Single<ISignalSubscriber>();
        }
        private void Start()
        {
            _subscriber.Subscribe<Signals.OnPickupHintStatusChanged>(ChangePickupHintStatus);
        }
        
        private void ChangePickupHintStatus(Signals.OnPickupHintStatusChanged data)
        {
            pickupHintText.text = data.IsShown ? $"Нажмите [E] чтобы подобрать {data.Item.ItemName}" : "";
            pickupHintText.enabled = data.IsShown;
        }
        
        private void OnDisable()
        {
            _subscriber.Unsubscribe<Signals.OnPickupHintStatusChanged>(ChangePickupHintStatus);
        }
    }
}