using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using UnityEngine;

namespace Source.Scripts.Game.Items
{
    //Сделать норм наследование
    public class WorldItem : MonoBehaviour, ICarried
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private ItemBase itemData;
        [SerializeField] private Outline outline;
        private ISignalSubscriber _subscriber;
        
        private void Awake()
        {
            _subscriber = AllServices.Container.Single<ISignalSubscriber>();
        }
        
        private void Start()
        {
            _subscriber.Subscribe<Signals.OnPickupHintStatusChanged>(ChangeOutlineStatus);
        }
        
        public IItem GetItem() => itemData;

        public void OnPickup()
        {
            gameObject.SetActive(false);
            itemData.OnPickup();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        
        public void OnDrop(Transform thrower, float force = 1f)
        {
            transform.SetParent(null);
            gameObject.SetActive(true);
            itemData.OnDrop();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(thrower.forward * force, ForceMode.Impulse);
        }

        private void ChangeOutlineStatus(Signals.OnPickupHintStatusChanged data)
        {
            if (!data.IsShown)
                outline.enabled = false;
            else
                if(data.Item.ItemName == itemData.ItemName)
                    outline.enabled = true;
        }
    }
}