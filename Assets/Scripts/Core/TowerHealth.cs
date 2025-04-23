using Unity.Netcode;
using UnityEngine;

public class TowerHealth : NetworkBehaviour
{
    public int maxHealth = 3;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            currentHealth.Value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer) return;

        currentHealth.Value -= damage;
        Debug.Log($"Tower took {damage} damage. Current health: {currentHealth.Value}");

        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Tower destroyed!");
        GameManagerUI.Instance.ShowLoseTextClientRpc(); // เรียกโชว์ข้อความ
        GetComponent<NetworkObject>().Despawn(); // ลบ Tower จาก Network
    }


    // สำหรับตรวจสอบว่ามีอะไรชน Tower
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Tower collided with: {collision.name}");
    }

    public int GetCurrentHealth()
    {
        return currentHealth.Value;
    }
}
