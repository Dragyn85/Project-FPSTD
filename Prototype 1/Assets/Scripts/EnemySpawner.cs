using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", 2, 2);
        Destroy(gameObject, 20);
    }
    
    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = transform.position;
    }
    
}
