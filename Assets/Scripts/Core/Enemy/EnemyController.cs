using Unity.Netcode;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{
    public int enemyHP = 10;
    public float moveSpeed = 2f;
    public float targetCheckRange = 3f;

    private Rigidbody2D rb;
    private Transform currentTarget;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            FindTarget();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!IsServer) return;

        FindTarget();

        if (currentTarget == null) return;

        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }
    }

    private void FindTarget()
    {
        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float towerDist = tower != null ? Vector2.Distance(transform.position, tower.transform.position) : float.MaxValue;
        float playerDist = player != null ? Vector2.Distance(transform.position, player.transform.position) : float.MaxValue;

        if (player != null && playerDist <= targetCheckRange && playerDist < towerDist)
        {
            currentTarget = player.transform;
        }
        else if (tower != null)
        {
            currentTarget = tower.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        // ถูกยิง
        if (collision.CompareTag("PlayerBullet"))
        {
            enemyHP--;
            if (enemyHP <= 0)
            {
                GetComponent<NetworkObject>().Despawn();
            }
            return;
        }

        // โจมตี Player
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(100);
            }

            GetComponent<NetworkObject>().Despawn();
            return;
        }

        // โจมตี Tower
        if (collision.CompareTag("Tower"))
        {
            TowerHealth towerHealth = collision.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(1);
            }

            GetComponent<NetworkObject>().Despawn();
        }
    }
}
