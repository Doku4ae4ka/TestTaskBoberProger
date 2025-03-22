using Source.Scripts.Game.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class InventoryItemHud : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        
        [SerializeField] private Image frameImage;
        [SerializeField] private Image backgroundImage;

        public void SelectItemV1() => backgroundImage.color = new Color(0f, 0.7f, 0.8f, 0.5f);

        public void DeselectItemV1() => backgroundImage.color = Color.white;

        public void SelectItemV2() => frameImage.enabled = true;

        public void DeselectItemV2() => frameImage.enabled = false;

        public void ShowItem(IItem item)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = item.Icon;
        }
        
        public void HideItem()
        {
            itemImage.gameObject.SetActive(false);
            itemImage.sprite = null;
        }
    }
}