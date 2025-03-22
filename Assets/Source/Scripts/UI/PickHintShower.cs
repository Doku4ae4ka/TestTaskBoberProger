using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class PickHintShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text pickHintText;
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
            pickHintText.text = data.IsShown ? $"Нажмите [E] чтобы подобрать {data.Item.ItemName}" : "";
            pickHintText.enabled = data.IsShown;
        }
        
        private void OnDestroy()
        {
            _subscriber.Unsubscribe<Signals.OnPickupHintStatusChanged>(ChangePickupHintStatus);
        }
    }
}