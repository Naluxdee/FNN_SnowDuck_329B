using UnityEngine;
using Unity.Netcode;

public class TriggerSpawn : NetworkBehaviour
{
    public GameObject objectToSpawn; // Prefab ที่มี NetworkObject
    public Transform spawnPosition;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return; // ให้ Server เท่านั้นเป็นคน Instantiate

        if (!hasSpawned && other.CompareTag("Player"))
        {
            GameObject spawned = Instantiate(objectToSpawn, spawnPosition.position, spawnPosition.rotation);
            spawned.GetComponent<NetworkObject>().Spawn(); // Spawn บน Network
            hasSpawned = true;
        }
    }
}
