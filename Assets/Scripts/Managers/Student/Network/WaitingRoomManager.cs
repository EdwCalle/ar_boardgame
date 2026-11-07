using TMPro;
using Unity.Netcode;
using UnityEngine;

public class WaitingRoomManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    NetworkManager m_NetworkManager;
    [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI roomCodeText;



    void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            m_NetworkManager = NetworkManager.Singleton;
            Debug.Log("NetworkManager encontrado. ¿Está conectado?: " + NetworkManager.Singleton.IsConnectedClient);
            roomNameText.text = "Room Name: " + Singleton.instance.CurrentSession.Name;
            roomCodeText.text = "Room Code: " + Singleton.instance.CurrentSession.Code;
            ShowPlayerList();
        }
    }

    void ShowPlayerList()
    {
        foreach (var client in m_NetworkManager.ConnectedClientsList)
        {
            Debug.Log("Jugador conectado: " + client.ClientId);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
