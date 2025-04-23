using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking; // หากใช้ UNet (แต่ไม่จำเป็นสำหรับ Netcode for GameObjects)

public class EnemyController : NetworkBehaviour
{
    public int enemyHP = 10;
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform targetPlayer;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Tower");
            if (player != null)
            {
                targetPlayer = player.transform;
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!IsServer || targetPlayer == null) return;

        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        // ⚠️ โดนกระสุน
        if (collision.CompareTag("PlayerBullet"))
        {
            enemyHP--;
            Debug.Log("Enemy hit!");

            if (enemyHP <= 0)
            {
                GetComponent<NetworkObject>().Despawn();
            }
        }

        // ⚠️ ชน Tower
        if (collision.CompareTag("Tower"))
        {
            TowerHealth tower = collision.GetComponent<TowerHealth>();
            if (tower != null)
            {
                tower.TakeDamage(1); // ลด HP Tower ลง 1
                Debug.Log("Enemy hit Tower! Tower HP reduced.");
            }

            GetComponent<NetworkObject>().Despawn(); // ทำลาย Enemy หลังชน
        }
    }
}
