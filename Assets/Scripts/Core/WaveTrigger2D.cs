using UnityEngine;
using Unity.Netcode;

public class WaveTrigger2D : NetworkBehaviour
{
    public OnlineEnemySpawnPoint spawner; // ลาก Script ที่ควบคุม Wave มาใส่

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer || triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;
            Debug.Log("[Server] Triggered wave start!");
            spawner.StartWaveServerRpc();
        }
    }
}
