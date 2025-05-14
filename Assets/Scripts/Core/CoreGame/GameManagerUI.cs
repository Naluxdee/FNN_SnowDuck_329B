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
        loseTextUI.SetActive(false);
    }

    [ClientRpc]
    public void ShowLoseTextClientRpc()
    {
        loseTextUI.SetActive(true);
    }

    public void LeaveGame()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            HostSingleton.Instance.GameManager.Shutdown();
        }

        ClientSingleton.Instance.GameManager.Disconnect();
    }
}
