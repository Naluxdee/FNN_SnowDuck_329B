using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : NetworkBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int maxHealth = 100;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            currentHealth.Value = maxHealth; // กำหนดเลือดเริ่มต้น
        }
    }

    [ServerRpc] // ฟังก์ชันนี้จะเรียกฝั่ง Server เท่านั้น
    public void TakeDamageServerRpc(int damage)
    {
        if (!IsServer) return; // ป้องกันการเรียกจาก Client

        currentHealth.Value -= damage;
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ลบ Enemy ออกจาก Network
        NetworkObject.Despawn();
        Destroy(gameObject);
    }
}
