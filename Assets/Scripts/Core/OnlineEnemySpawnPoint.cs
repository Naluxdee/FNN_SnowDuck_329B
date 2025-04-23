using UnityEngine;
using Unity.Netcode;

public class OnlineEnemySpawnPoint : NetworkBehaviour
{
    public GameObject enemyPrefab; // ใส่ Prefab ที่มี NetworkObject
    public Transform[] spawnPoints; // จุดเกิดของ Enemy

    public float spawnInterval = 5f;
    private float timer;

    void Update()
    {
        if (!IsServer) return; // ให้ Server เท่านั้นเป็นคน Spawn

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
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
