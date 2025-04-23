using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
// �ҡ�� UNet

public class EnemyController : NetworkBehaviour
{
    /*public int enemyHP = 10;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform targetPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �� Tower ��� Spawn ��
        if (IsServer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Tower");
            if (player != null)
            {
                targetPlayer = player.transform;
            }
        }
    }


    void FixedUpdate()
    {
        if (!IsServer || targetPlayer == null) return;

        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f; // -90 ������� sprite �ѹ��������������
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Debug.Log("Enemy hit by Player Bullet!");
            enemyHP--;

            // ��Ҿ�ѧ���Ե��� ������� Enemy
            if (enemyHP <= 0)
            {
                NetworkBehaviour.Destroy(gameObject);
            }
            
        }
        
    }*/
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

        if (collision.CompareTag("PlayerBullet"))
        {
            enemyHP--;
            Debug.Log("Enemy hit!");

            if (enemyHP <= 0)
            {
                GetComponent<NetworkObject>().Despawn();
            }
        }
    }
}