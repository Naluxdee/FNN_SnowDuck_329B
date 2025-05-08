using UnityEngine;
using System.Collections;

public class TowerPathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float stopTimeAtWaypoint = 1f;

    private int currentWaypoint = 0;
    private bool isMoving = false;

    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveAlongWaypoints());
        }
    }

    IEnumerator MoveAlongWaypoints()
    {
        while (currentWaypoint < waypoints.Length)
        {
            Transform target = waypoints[currentWaypoint];
            while (Vector2.Distance(transform.position, target.position) > 0.05f)
            {
                // หาทิศทาง
                Vector2 direction = (target.position - transform.position).normalized;

                // หมุนให้หันไปทิศทางการเคลื่อนที่ (แนวตั้งขึ้น = 0 องศา)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

                // เดิน
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(stopTimeAtWaypoint);
            currentWaypoint++;
        }

        Debug.Log("Tower finished moving through all waypoints!");
    }
}