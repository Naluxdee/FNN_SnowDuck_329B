using Unity.Netcode;
using UnityEngine;
// �ҡ�� UNet
using UnityEngine.Networking;

public class TowerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    //[SyncVar] // ����Ѻ UNet/Mirror: �ԧ���Ҿ�ѧ���Ե
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Tower took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Tower destroyed!");
        // ����͹�Ź� �س�Ҩ�е�ͧ���������蹷�Һ��� Tower �١���������
        // ����Ҩ�� Logic ���� �������Ǣ�ͧ�Ѻ����骹�
        // �ҡ�� UNet: NetworkServer.Destroy(gameObject);
        Destroy(gameObject); // ����Ѻ��÷��ͺ Offline
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("it hit!!!");
    }
}