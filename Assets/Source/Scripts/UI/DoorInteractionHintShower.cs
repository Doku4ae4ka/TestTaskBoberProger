using Source.Scripts.Game.Items;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class DoorInteractionHintShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text interactionHintText;
        private ISignalSubscriber _subscriber;

        private void Awake()
        {
            _subscriber = AllServices.Container.Single<ISignalSubscriber>();
        }
        private void Start()
        {
            _subscriber.Subscribe<Signals.OnDoorHintStatusChanged>(ChangeDoorHintStatus);
        }
        
        private void ChangeDoorHintStatus(Signals.OnDoorHintStatusChanged data)
        {
            if (data.IsShown && data.Item != null)
            {
                var hasKey = false;
                var keyItem = data.Item as KeyItem;
                if (keyItem && keyItem.keyId == data.Locker.KeyId)
                    hasKey = true;

                if (hasKey && data.Locker.DoorLocked)
                    interactionHintText.text = "Ключ подошел! Нажмите [E] чтобы открыть замок";
                else if (!hasKey && data.Locker.DoorLocked)
                    interactionHintText.text = $"Мне нужно найти {data.Locker.GetItem().ItemName}, чтобы открыть дверь";
                else
                    interactionHintText.text = "Нажмите [E] чтобы открыть дверь";
            }
            if (data.IsShown && data.Item == null)
                interactionHintText.text = $"Мне нужно найти {data.Locker.GetItem().ItemName}, чтобы открыть дверь";
            interactionHintText.enabled = data.IsShown;
        }
        
        private void OnDestroy()
        {
            _subscriber.Unsubscribe<Signals.OnDoorHintStatusChanged>(ChangeDoorHintStatus);
        }
    }
}