using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab; // Prefab �ͧ�ѵ��
    public Transform spawnPoint; // �ش����ͧ�������ѵ���Դ
    private bool hasSpawned = false; // ��ͧ�ѹ��� Spawn ���

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsServer && !hasSpawned && other.CompareTag("Player")) // ������� Host/Server ��� Player �Թ���
        {
            SpawnEnemy();
            hasSpawned = true; // ��ͧ�ѹ��� Spawn ���
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<NetworkObject>().Spawn(); // �ԧ����ѧ�ء Client
        Debug.Log("[SERVER] Enemy Spawned!");
    }
}
