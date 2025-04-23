using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class OnlineEnemySpawnPoint : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 10f;
    public float timeBetweenSpawns = 1f;
    public int enemiesPerWaveStart = 3;
    public int enemiesIncrementPerWave = 2;
    public int maxWaves = 5; // 🌟 จำนวน Wave สูงสุด

    private int currentWave = 0;
    private bool waveStarted = false;

    [ServerRpc(RequireOwnership = false)]
    public void StartWaveServerRpc()
    {
        if (!waveStarted)
        {
            waveStarted = true;
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < maxWaves)
        {
            currentWave++;
            int enemiesToSpawn = enemiesPerWaveStart + enemiesIncrementPerWave * (currentWave - 1);
            Debug.Log($"[Server] Starting Wave {currentWave}/{maxWaves}, Spawning {enemiesToSpawn} enemies");

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("[Server] All waves completed!");
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int index = Random.Range(0, spawnPoints.Length);
        Vector3 pos = spawnPoints[index].position;

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn();
    }
}
