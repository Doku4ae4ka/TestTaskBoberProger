using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Source.Scripts.Game.Inventory.Items
{
    public abstract class ItemBase : ScriptableObject, IItem
    {
        [Header("Основные настройки предмета")]
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private GameObject prefab;
        
        public string ItemName => itemName;
        public Sprite Icon => icon;
        public GameObject Prefab => prefab;
        
        public void OnPickup()
        {
        }

        public void OnDrop()
        {
        }
    }
}