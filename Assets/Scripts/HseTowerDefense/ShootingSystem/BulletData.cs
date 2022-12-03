using UnityEngine;

namespace HseTowerDefense.ShootingSystem
{
    [CreateAssetMenu(fileName = "New Bullet Data", menuName = "HSE Tower Defense/Shooting System/Bullet Data", order = 51)]
    public class BulletData : ScriptableObject
    {
        [Header("Settings")] 
        [SerializeField] private float _flightSpeed;
        [SerializeField] private float _damage;

        public float Damage => _damage;

        public float FlightSpeed => _flightSpeed;
    }
}