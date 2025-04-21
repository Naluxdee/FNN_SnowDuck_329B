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
            currentHealth.Value = maxHealth; // ��˹����ʹ�������
        }
    }

    [ServerRpc] // �ѧ��ѹ�������¡��� Server ��ҹ��
    public void TakeDamageServerRpc(int damage)
    {
        if (!IsServer) return; // ��ͧ�ѹ������¡�ҡ Client

        currentHealth.Value -= damage;
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ź Enemy �͡�ҡ Network
        NetworkObject.Despawn();
        Destroy(gameObject);
    }
}
