using UnityEngine;
using Unity.Netcode;

public class TriggerSpawn : NetworkBehaviour
{
    public GameObject objectToSpawn; // Prefab ����� NetworkObject
    public Transform spawnPosition;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return; // ��� Server ��ҹ���繤� Instantiate

        if (!hasSpawned && other.CompareTag("Player"))
        {
            GameObject spawned = Instantiate(objectToSpawn, spawnPosition.position, spawnPosition.rotation);
            spawned.GetComponent<NetworkObject>().Spawn(); // Spawn �� Network
            hasSpawned = true;
        }
    }
}
