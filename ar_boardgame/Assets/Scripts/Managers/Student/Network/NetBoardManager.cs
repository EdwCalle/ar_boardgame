using Photon.Pun;
using TMPro;
using UnityEngine;

public class NetBoardManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI LogMessage;
    float offsetTime = 2f;
    float nextTime;
    void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        {
            SendLogMessage($"Number players:{PhotonNetwork.CurrentRoom.Players.Count} RoomName:{PhotonNetwork.CurrentRoom.Name} Nickname:{PhotonNetwork.LocalPlayer.NickName}");
            // Singleton.instance
        }
    }
    public void SendLogMessage(string message)
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        {
            if (Time.time >= nextTime)
            {

                if (LogMessage != null)
                {
                    LogMessage.text = $"Log:{message}\n";
                }

                nextTime = Time.time + offsetTime;
            }
        }
    }
    void Update()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
            SendLogMessage($"Number players:{PhotonNetwork.CurrentRoom.Players.Count} RoomName:{PhotonNetwork.CurrentRoom.Name} Nickname:{PhotonNetwork.LocalPlayer.NickName} LocalPlayer:{PhotonNetwork.CurrentRoom.PlayerCount}");

    }
}
