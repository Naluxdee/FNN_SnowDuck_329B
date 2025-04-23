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
        GameManagerUI.Instance.ShowLoseTextClientRpc(); // ���¡����ͤ���
        GetComponent<NetworkObject>().Despawn(); // ź Tower �ҡ Network
    }


    // ����Ѻ��Ǩ�ͺ��������ê� Tower
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Tower collided with: {collision.name}");
    }

    public int GetCurrentHealth()
    {
        return currentHealth.Value;
    }
}
