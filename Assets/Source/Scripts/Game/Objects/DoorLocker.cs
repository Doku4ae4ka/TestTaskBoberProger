using UnityEngine;

namespace Source.Scripts.Game.Objects
{
    public class DoorLocker : MonoBehaviour
    {
        [SerializeField] private bool doorLocked;

        private void Update()
        {
            if (doorLocked) return;
        }
    }
}