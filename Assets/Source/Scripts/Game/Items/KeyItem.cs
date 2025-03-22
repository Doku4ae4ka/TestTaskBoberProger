using UnityEngine;

namespace Source.Scripts.Game.Items
{
    [CreateAssetMenu(menuName = "Items/Key Item", fileName = "NewKeyItem")]
    public class KeyItem : ItemBase
    {
        public int keyId;
    }
}