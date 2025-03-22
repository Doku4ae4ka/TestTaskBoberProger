using UnityEngine;

namespace Source.Scripts.Game.Inventory.Items
{
    public class WorldItem : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private ItemBase itemData;
        
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

    }
}