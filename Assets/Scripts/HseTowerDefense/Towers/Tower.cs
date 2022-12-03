using HseTowerDefense.ShootingSystem;
using UnityEngine;

namespace HseTowerDefense.Towers
{
    public class Tower : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Transform _rotationObjectTransform;
        [SerializeField] private Transform _bulletSpawnPointTransform;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private EnemyController _targetEnemy;
        
        [Header("Settings")] 
        [SerializeField] private LayerMask _physicsLayerMask;
        [SerializeField, Range(0, 100)] private float _attackRadius;
        [SerializeField, Range(0, 5)] private float _shootingDelay;
        
        
        private float _shootDelay;

        private bool Shoot()
        {
            if (_targetEnemy == null)
                return false;

            GameObject bulletGm = Instantiate(_bulletPrefab.gameObject);
            bulletGm.transform.position = _bulletSpawnPointTransform.position;
            bulletGm.transform.LookAt(_targetEnemy.transform);
            Bullet bullet = bulletGm.GetComponent<Bullet>();
            bullet.Init(_targetEnemy.transform);
            
            return true;
        }
        
        private void Update()
        {
            if (_shootDelay <= 0)
            {
                if (Shoot())
                {
                    _shootDelay = _shootingDelay;
                }
            }
            else
            {
                _shootDelay -= Time.deltaTime;
            }
            
            if (_targetEnemy == null)
            {
                Collider[] ememies = Physics.OverlapSphere(_rotationObjectTransform.position, _attackRadius, _physicsLayerMask);
                if (ememies.Length != 0)
                {
                    _targetEnemy = ememies[0].GetComponent<EnemyController>();
                }
            }
            else
            {
                _rotationObjectTransform.LookAt(_targetEnemy.transform);
                
                if (Vector3.Distance(_rotationObjectTransform.position, _targetEnemy.transform.position) > _attackRadius)
                {
                    _targetEnemy = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_rotationObjectTransform.position, _attackRadius);
        }
    }
}