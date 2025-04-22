using Unity.Netcode;
using UnityEngine;
// หากใช้ UNet
using UnityEngine.Networking;

public class TowerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    //[SyncVar] // สำหรับ UNet/Mirror: ซิงค์ค่าพลังชีวิต
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
        // ในเกมออนไลน์ คุณอาจจะต้องแจ้งให้ผู้เล่นทราบว่า Tower ถูกทำลายแล้ว
        // และอาจมี Logic อื่นๆ ที่เกี่ยวข้องกับการแพ้ชนะ
        // หากใช้ UNet: NetworkServer.Destroy(gameObject);
        Destroy(gameObject); // สำหรับการทดสอบ Offline
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("it hit!!!");
    }
}