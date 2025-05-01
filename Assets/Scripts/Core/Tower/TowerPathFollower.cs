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
/*    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;
    }
*/
    IEnumerator MoveAlongWaypoints()
    {
        while (currentWaypoint < waypoints.Length)
        {
            Transform target = waypoints[currentWaypoint];
            while (Vector2.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(stopTimeAtWaypoint); // À¬ÿ¥√Õ∑’Ë waypoint

            currentWaypoint++;
        }

        Debug.Log("Tower finished moving through all waypoints!");
    }
}
