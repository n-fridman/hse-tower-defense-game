using UnityEngine;

namespace HseTowerDefense.PlayerResources
{
    public class PlayerInventory : MonoBehaviour
    {
        public float coins;
        
        public static PlayerInventory Instance;

        private void Awake()
        {
            Instance = this;
            coins = 250;
        }
    }
}