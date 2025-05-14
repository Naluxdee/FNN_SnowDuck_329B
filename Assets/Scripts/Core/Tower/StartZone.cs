using UnityEngine;
using System.Collections;
using TMPro;

public class StartZone : MonoBehaviour
{
    public TowerPathFollower TowerPathFollower; 
    public TextMeshProUGUI countdownText;       

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("Player entered Start Zone!");
            StartCoroutine(CountdownAndStart());
        }
    }

    private IEnumerator CountdownAndStart()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(true);

        float countdown = 3f;

        while (countdown > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.CeilToInt(countdown).ToString();

            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        if (countdownText != null)
        {
            countdownText.text = "Go!";
        }

        yield return new WaitForSeconds(0.5f);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        TowerPathFollower.StartMoving();

        Destroy(gameObject);
    }
}
