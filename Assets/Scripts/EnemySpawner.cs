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
    private int waveIndex = 0;
    
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<WaveData> waveData = new List<WaveData>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float waveCoolTime = 10f;

    List<GameObject> enemys = new List<GameObject>();
    private float lastWaveTime = 0;

    private Coroutine waveCoroutine;


    public void ResetWave()
    {
        StopWave();

        foreach (var enemy in enemys)
        {
            if(enemy != null)
                Destroy(enemy);
        }
        enemys.Clear();

		waveIndex = 0;
		lastWaveTime = Time.time + waveCoolTime;
	}

	public void StopWave()
	{
		if (waveCoroutine != null)
		{
			StopCoroutine(waveCoroutine);
			waveCoroutine = null;
		}
	}

	private void Update()
    {
        
        if (lastWaveTime <= Time.time && !RunManager.Instance.EndRunning || waveData.Count > waveIndex)
        {
            StopWave();
			waveCoroutine = StartCoroutine(StartWave());
            lastWaveTime = Time.time + waveCoolTime;
            waveIndex++;
        }
    }

    private IEnumerator StartWave()
    {
        //에러 방지
        int currentWaveIndex = waveIndex;
        if (waveIndex >= waveData.Count)
        {
            currentWaveIndex = waveData.Count - 1;
		}

        var currentWaveData = waveData[currentWaveIndex];


		for (int i = 0; i < currentWaveData.spawnCount; i++)
        {
            Transform pos = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(enemyPrefab, pos.position, Quaternion.identity);
            enemys.Add(enemy);
			yield return new WaitForSeconds(currentWaveData.spawnCoolTime);
        }
    }
}