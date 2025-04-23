using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : NetworkBehaviour
{
    public static GameManagerUI Instance;

    [SerializeField] private GameObject loseTextUI;

    private void Awake()
    {
        Instance = this;
        loseTextUI.SetActive(false); // �Դ����͹
    }

    [ClientRpc]
    public void ShowLoseTextClientRpc()
    {
        loseTextUI.SetActive(true);
    }
}
