using System.Collections.Generic;
using HseTowerDefense.Enemies;
using HseTowerDefense.PlayerResources;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Configuration")] 
    [SerializeField] private EnemyData _enemyData;

    [SerializeField] private Vector3 _healthSliderOffset;
    [SerializeField] private Slider _healthSlider;
    
    [Header("Settings")] 
    [Tooltip("Current enemy state.")]
    [SerializeField] private EnemyState _enemyState;

    [SerializeField] private float _health;

    /// <summary>
    /// Enemy movement points.
    /// </summary>
    private Queue<Transform> _wayPoints = new();
    
    /// <summary>
    /// Next point to move.
    /// </summary>
    private Transform _nextPoint;
    
    /// <summary>
    /// Transform reference.
    /// </summary>
    private Transform _transform;

    public EnemyState State => _enemyState;
    
    private void Awake()
    {
        _enemyState = EnemyState.Idle;
        _health = _enemyData.Health;
    }

    private void Update()
    {
        if (_healthSlider != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(_transform.position);
            screenPosition += _healthSliderOffset;

            _healthSlider.transform.position = screenPosition;

            float normalizedHealth = _health / _enemyData.Health;
            _healthSlider.value = normalizedHealth;
        }
        
        if (_enemyState == EnemyState.Finish || _enemyState == EnemyState.Idle)
            return;

        if (_transform.position != _nextPoint.position)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _nextPoint.position, _enemyData.Speed * Time.deltaTime);
        }
        else
        {
            if (_wayPoints.Count == 0)
            {
                _enemyState = EnemyState.Finish;
            }
            else
            {
                _nextPoint = _wayPoints.Dequeue();
                _transform.LookAt(_nextPoint);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _enemyState = EnemyState.Die;
            gameObject.SetActive(false);
            PlayerInventory.Instance.coins += _enemyData.KillCost;
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Set waypoints for enemy movement.
    /// </summary>
    /// <param name="path">Way points list.</param>
    public void SetPath(List<Transform> path)
    {
        foreach (Transform wayPoint in path)
        {
            _wayPoints.Enqueue(wayPoint);
        }
        
        _nextPoint = _wayPoints.Dequeue();
        _transform = transform;
        _transform.LookAt(_nextPoint);
        
        _enemyState = EnemyState.Moving;
    }
}
