using Unity.Netcode;
using UnityEngine;
// �ҡ�� UNet
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{
    public int enemyHP = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("it hit!!!");

        NetworkBehaviour.Destroy(gameObject);

    }
}