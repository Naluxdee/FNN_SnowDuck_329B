using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class HealOverTimeItem : NetworkBehaviour
{
    public int healAmountPerTick = 20;
    public float tickInterval = 1f;
    public int totalTicks = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null && health.CurrentHealth.Value < health.MaxHealth)
            {
                StartCoroutine(HealOverTime(health));

                // ทำลาย item ทิ้งหลังใช้งาน
                GetComponent<NetworkObject>().Despawn();
            }
        }
    }

    private IEnumerator HealOverTime(Health health)
    {
        for (int i = 0; i < totalTicks; i++)
        {
            if (health != null)
            {
                health.RestoreHealth(healAmountPerTick);
            }

            yield return new WaitForSeconds(tickInterval);
        }
    }
}
