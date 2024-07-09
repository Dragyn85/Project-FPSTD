using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;
    
    private Queue<Wave> waveQueue;
    float nextWaveStartTime;
    private Wave currentWave;
    public bool spawningActive = false;
    
    
    public void Start()
    {
        waveQueue = new Queue<Wave>();
        foreach (var wave in waves)
        {
            waveQueue.Enqueue(wave);
        }
        StartSpawning();
    }

    public void StartSpawning()
    {
        spawningActive = true;
        currentWave = waveQueue.Dequeue();
        currentWave.StartWave();
        nextWaveStartTime = Time.time + currentWave.WaveDelay;
        currentWave.OnWaveComplete += OnWaveComplete;
    }

    private void OnWaveComplete(Wave obj)
    {
        obj.OnWaveComplete -= OnWaveComplete;
        if (waveQueue.Count == 0)
        {
            Debug.Log("All waves complete");
            spawningActive = false;
        }
        else
        {
            currentWave = waveQueue.Dequeue();
            currentWave.StartWave();
            nextWaveStartTime = Time.time + currentWave.WaveDelay;
            currentWave.OnWaveComplete += OnWaveComplete;
        }
    }

    private void Update()
    {
        if(spawningActive == false) return;
        
        if (nextWaveStartTime <= Time.time)
        {
            currentWave.Tick(Time.deltaTime);
        }
    }
}

[Serializable]
public class Wave
{
    public float WaveDelay;
    public event Action<Wave> OnWaveComplete;
    public List<Pulses> enemyPulses;

    Queue<List<EnemyPulse>> enemyPulseQueue;

    public List<EnemyPulse> currentPulses;

    private void FillPulseQueue()
    {
        enemyPulseQueue = new Queue<List<EnemyPulse>>();
        for (var index = 0; index < enemyPulses.Count; index++)
        {
            var enemyPulse = enemyPulses[index];
            enemyPulseQueue.Enqueue(enemyPulse.pulses);
        }
    }

    public void StartWave()
    {
        FillPulseQueue();
        StartNextPulse();
    }

    private void StartNextPulse()
    {
        if (enemyPulseQueue.Count == 0)
        {
            Debug.Log("Wave complete");
            OnWaveComplete?.Invoke(this);
        }
        else
        {
            currentPulses = enemyPulseQueue.Dequeue();
            foreach (var pulse in currentPulses)
            {
                //pulse.OnPulseComplete += OnPulseComplete;
            }
        }
    }

    private void OnPulseComplete(EnemyPulse pulse)
    {
        pulse.OnPulseComplete -= OnPulseComplete;
        currentPulses.Remove(pulse);
        if (currentPulses.Count == 0)
        {
            StartNextPulse();
        }
    }


    public void Tick(float deltaTime)
    {
        foreach (var pulse in currentPulses)
        {
            pulse.Tick(deltaTime);
        }
        bool isComplete = true;
        foreach (var pulse in currentPulses)
        {
            if (!pulse.IsComplete)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            StartNextPulse();
        }
    }
}

[Serializable]
public struct Pulses
{
    public List<EnemyPulse> pulses;
}

[Serializable]
public class EnemyPulse
{
    public event Action<EnemyPulse> OnPulseComplete;

    public GameObject enemyPrefab;
    public SpawnArea spawnAreas;
    public float initialDelay;
    public float spawnInterval;
    public int amountToSpawn;

    private bool spawnEnemys = true;
    private float nextSpawnTime = 0;
    
    public bool IsComplete => amountToSpawn <= 0;

    public void StartSpawning()
    {
        spawnEnemys = true;
        nextSpawnTime = Time.time + initialDelay;
    }

    public void Tick(float deltaTime)
    {
        if (!spawnEnemys) return;
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;
            
            var pos = NavMesh.SamplePosition(spawnAreas.GetSpawnPosition(),out NavMeshHit hit,  3, NavMesh.AllAreas);
            GameObject enemy = GameObject.Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            //enemy.transform.position = hit.position;
            Debug.Log($"Spawned at {spawnAreas.name} spawn area {spawnAreas.GetSpawnPosition()} enemy pos {enemy.transform.position}");
            amountToSpawn--;
            if (amountToSpawn <= 0)
            {
                spawnEnemys = false;
                Debug.Log("Pulse Completet");
                //OnPulseComplete?.Invoke(this);
            }
        }
    }
}