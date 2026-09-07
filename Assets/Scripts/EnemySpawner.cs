using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct WaveData
{
    public int spawnCount;
    public float spawnCoolTime;
    
}

public class EnemySpawner : MonoBehaviour
{
    private int wave = 0;
    
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<WaveData> waveData = new List<WaveData>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float waveCoolTime = 10f;
    
    private float _spawnCoolTime = 1f;

    private float lastWaveTime = 0;

    private void Update()
    {
        
        if (lastWaveTime <= Time.time && !RunManager.Instance.EndRunning)
        {
            StartCoroutine(StartWave());
            lastWaveTime = Time.time + waveCoolTime;
            wave++;
        }
    }

    private IEnumerator StartWave()
    {
        /*float spawnTime = waveData[wave].spawnCoolTime;
        float spawnCount = waveData[wave].spawnCount;
        if (spawnTime * spawnCount >= waveCoolTime)
        {
            //총 스폰 시간이 웨이브 시간보다 길 때 처리    
        }*/
        
        for (int i = 0; i < waveData[wave].spawnCount; i++)
        {
            Transform pos = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(enemyPrefab, pos.position, Quaternion.identity);
                
            yield return new WaitForSeconds(waveData[wave].spawnCoolTime);
            if (RunManager.Instance.EndRunning)
                break;
        }
    }
}