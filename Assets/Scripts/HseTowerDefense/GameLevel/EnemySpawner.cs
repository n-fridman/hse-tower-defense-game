using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HseTowerDefense.Enemies;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyWave
    {
        public string waveName;
        public EnemyData enemyData;
        public float spawnDelay;
        [Range(0, 50)] public int count;
    }
    
    [Header("Way points")] 
    [SerializeField] private List<Transform> _wayPoints;

    [Header("Spawner settings")]
    [SerializeField] private Transform _enemyParentTransform;
    [SerializeField] private Transform _spawnerPointTransform;
    [SerializeField] private List<EnemyWave> _enemyConfigs; 
    [SerializeField] private float _waveDelay;

    private CancellationTokenSource _spawnerCancellationToken;

    private void Start()
    {
        _spawnerCancellationToken = new CancellationTokenSource();
        Spawner(_spawnerCancellationToken.Token);
    }

    private async void Spawner(CancellationToken cancellationToken)
    {
        try
        {
            for (int i = 0; i < _enemyConfigs.Count; i++)
            {
                EnemyWave wave = _enemyConfigs[i];
            
                SpawnWave(wave, cancellationToken);
            
                int delay = (int) (_waveDelay * 1000);
                await Task.Delay(delay, cancellationToken);
            }   
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Spanwing canceled.");
        }
    }
    
    private async void SpawnWave(EnemyWave wave, CancellationToken cancellationToken)
    {
        try
        {
            for (int i = wave.count - 1; i >= 0; i--)
            {
                GameObject prefab = wave.enemyData.ViewPrefab;
                GameObject enemyGm = Instantiate(prefab, _enemyParentTransform);
                enemyGm.transform.position = _spawnerPointTransform.position;
                EnemyController enemyController = enemyGm.GetComponent<EnemyController>();
                enemyController.SetPath(_wayPoints);
                int delay = (int) (wave.spawnDelay * 1000);
                await Task.Delay(delay, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Spanwing canceled.");
        }
    }

    private void OnDestroy()
    {
        if (_spawnerCancellationToken != null)
        {
            _spawnerCancellationToken.Cancel();
        }
    }
}
