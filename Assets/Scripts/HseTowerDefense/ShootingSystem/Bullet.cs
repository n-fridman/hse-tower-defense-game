using HseTowerDefense.ShootingSystem;
using UnityEngine;

namespace HseTowerDefense.ShootingSystem
{
    public class Bullet : MonoBehaviour
    {
        [Header("Configuration")] 
        [SerializeField] private BulletData _bulletData;
        
        [Header("Components")] 
        [SerializeField] private Transform _targetTransform;


        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void Init(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        private void Update()
        {
            if (_targetTransform != null)
            {
                float dt = Time.deltaTime;
                float speed = _bulletData.FlightSpeed;
                Vector3 position = _transform.position;
                Vector3 targetPosition = _targetTransform.position;
                
                _transform.position = Vector3.MoveTowards(position, targetPosition, speed * dt);
                _transform.LookAt(_targetTransform);
                
                if (_transform.position == _targetTransform.position)
                {
                    if (_targetTransform.TryGetComponent(out  EnemyController enemy))
                    {
                        enemy.TakeDamage(_bulletData.Damage);
                        gameObject.SetActive(false);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}