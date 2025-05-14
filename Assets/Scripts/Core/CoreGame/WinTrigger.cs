using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public GameObject winTextObject; 

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Tower"))
        {
            triggered = true;

            if (winTextObject != null)
            {
                winTextObject.SetActive(true);
            }

            Debug.Log("You Win!");
        }
    }
}
