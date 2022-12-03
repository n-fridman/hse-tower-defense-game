using System;
using UnityEngine;

namespace HseTowerDefense.BuildingSystem
{
    [RequireComponent(typeof(BoxCollider))]
    public class BuildingCell : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private BoxCollider _boxCollider;
        
        [Header("Settings")]
        [SerializeField] private bool _canBuilding;

        public bool CanBuilding => _canBuilding;

        public void SetCanBuilding(bool canBuilding)
        {
            _canBuilding = canBuilding;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            if (_boxCollider != null)
            {
                Gizmos.DrawWireCube(transform.position, _boxCollider.size);
            }
        }
    }
}