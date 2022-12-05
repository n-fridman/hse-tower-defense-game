using UnityEngine;

namespace HseTowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "HSE Tower Defense/Enemies/EnemyData", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [Header("Movement settings")] 
        [Tooltip("Enemy movement speed.")]
        [SerializeField] private float _speed;

        [Header("Fight settings")]
        [Tooltip("Enemy health.")]
        [SerializeField] private float _health;

        [Header("View settings")] 
        [Tooltip("Enemy view prefab.")]
        [SerializeField] private GameObject viewPrefab;

        [SerializeField] private float _killCost;

        public float KillCost => _killCost;
        
        /// <summary>
        /// Enemy movement speed.
        /// </summary>
        public float Speed => _speed;
        
        /// <summary>
        /// Enemy movement health.
        /// </summary>
        public float Health => _health;
        
        /// <summary>
        /// Enemy view prefab.
        /// </summary>
        public GameObject ViewPrefab => viewPrefab;
    }
}