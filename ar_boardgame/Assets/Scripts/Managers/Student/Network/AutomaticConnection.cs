
using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class AutomaticConnection : MonoBehaviourPunCallbacks
{
    //public string scene;
    public TextMeshProUGUI LogMessage;
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            // Destroy(this);
            // SendLogMessage($"Number players:{PhotonNetwork.CurrentRoom.Players.Count}\n RoomName:{PhotonNetwork.CurrentRoom.Name}");
        }


    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(customLobby);

    }
    public override void OnJoinedLobby()
    {
        CreateRoom();
    }
    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("sala_1", new RoomOptions()
        {
            MaxPlayers = 4,
            IsVisible = true,
            IsOpen = true
        }, customLobby);
    }
    public override void OnJoinedRoom()
    {
        SendLogMessage("Entrando a la sala");
        PhotonNetwork.LoadLevel("board");

    }
    public override void OnLeftRoom()
    {

    }
    public void SendLogMessage(String message)
    {
        if (LogMessage != null)
        {
            LogMessage.text = $"Log:{message}";
        }
    }



}
