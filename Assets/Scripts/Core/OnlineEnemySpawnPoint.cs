using UnityEngine;
using Unity.Netcode;

public class OnlineEnemySpawnPoint : NetworkBehaviour
{
    public GameObject enemyPrefab; // ��� Prefab ����� NetworkObject
    public Transform[] spawnPoints; // �ش�Դ�ͧ Enemy

    public float spawnInterval = 5f;
    private float timer;

    void Update()
    {
        if (!IsServer) return; // ��� Server ��ҹ���繤� Spawn

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
