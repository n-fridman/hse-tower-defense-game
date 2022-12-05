using HseTowerDefense.Towers;
using UnityEngine;
using UnityEngine.Serialization;

namespace HseTowerDefense.BuildingSystem
{
    [CreateAssetMenu(fileName = "New Tower Data", menuName = "HSE Tower Defense/Building System/Tower data", order = 51)]
    public class TowerData : ScriptableObject
    {
        [Header("Settings")] 
        [SerializeField] private Sprite _towerIcon;
        [SerializeField] private float _price;
        [SerializeField] private Tower _towerPrefab;
        [SerializeField] private GameObject _towerPreviewPrefab;

        public Sprite TowerIcon => _towerIcon;
        
        public float Price => _price;

        public Tower TowerPrefab => _towerPrefab;

        public GameObject TowerPreviewPrefab => _towerPreviewPrefab;
    }
}