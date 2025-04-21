using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab; // Prefab ของศัตรู
    public Transform spawnPoint; // จุดที่ต้องการให้ศัตรูเกิด
    private bool hasSpawned = false; // ป้องกันการ Spawn ซ้ำ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsServer && !hasSpawned && other.CompareTag("Player")) // เช็กว่าเป็น Host/Server และ Player เดินเข้า
        {
            SpawnEnemy();
            hasSpawned = true; // ป้องกันการ Spawn ซ้ำ
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn(); // ซิงค์ไปยังทุก Client
        Debug.Log("[SERVER] Enemy Spawned!");
    }
}
