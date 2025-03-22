using DG.Tweening;
using Source.Scripts.Game.Inventory.Items;
using UnityEngine;

namespace Source.Scripts.Game.Inventory
{
    public class HandsDisplay : MonoBehaviour
    {
        [Header("Зависимости")]
        [SerializeField] private Inventory inventory;
        
        [Header("Настройки отображения предмета в руке")]
        [Tooltip("Позиция, где предмет будет отображаться в руке")]
        [SerializeField] private Transform handTransform;
        [Tooltip("Точка, куда предмет плавно перемещается, чтобы скрыться (и откуда появляется)")]
        [SerializeField] private Transform offScreenTransform;
        [Tooltip("Длительность анимации перехода")]
        [SerializeField] private float animationDuration = 0.5f;
        
        private WorldItem _currentItemObject;
        private int _currentSlot;
        private Tween _currentTween;
        private bool _hasObject;

        public void DisplayItem(int slot)
        {
            var hasOldObject = inventory.TryGetWorldItem(inventory.SelectedSlotIndex, out var oldObject);
            var hasNewObject = inventory.TryGetWorldItem(slot, out var newObject);

            if (_currentTween != null && _currentTween.IsActive())
            {
                _currentTween.Kill();
                
                oldObject.gameObject.SetActive(false);
                newObject.gameObject.SetActive(false);
            }

            if (hasOldObject && hasNewObject)
            {
                oldObject.transform.SetParent(offScreenTransform);
                oldObject.transform.DOMove(offScreenTransform.position, animationDuration)
                    .OnComplete(() =>
                    {
                        oldObject.gameObject.SetActive(false);
                        ActivateAndAnimateNewItem(newObject);
                    });
            }
            else if (!hasOldObject && hasNewObject)
            {
                ActivateAndAnimateNewItem(newObject);
            }
            else if (hasOldObject && !hasNewObject)
            {
                oldObject.transform.SetParent(offScreenTransform);
                oldObject.transform.DOMove(offScreenTransform.position, animationDuration)
                    .OnComplete(() =>
                    {
                        oldObject.gameObject.SetActive(false);
                    });
            }
        }
        
        private void ActivateAndAnimateNewItem(WorldItem newObject)
        {
            newObject.transform.SetParent(handTransform);
            newObject.gameObject.SetActive(true);
            newObject.transform.position = offScreenTransform.position;
            newObject.transform.DOMove(handTransform.position, animationDuration);
        }
    }
}