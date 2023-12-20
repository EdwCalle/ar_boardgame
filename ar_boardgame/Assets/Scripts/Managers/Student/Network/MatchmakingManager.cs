
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI nameCreatedRoom;
    public TextMeshProUGUI nameJoinedRoom;
    public TextMeshProUGUI message;
    public bool activateServer = false;
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    void Start()
    {
        if (activateServer)
        {
            PhotonNetwork.NickName = "evder";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            PhotonNetwork.JoinLobby(customLobby);



    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }


    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom(nameCreatedRoom.text, new RoomOptions()
        {
            MaxPlayers = 2,
            IsVisible = true,
            IsOpen = true
        });
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(nameJoinedRoom.text);
    }
    public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        // PhotonNetwork.NickName = "Player" + PhotonNetwork.CurrentRoom.PlayerCount;
        ScoreManager.BeginScore(PhotonNetwork.LocalPlayer);
        Singleton.instance.boardPersistent.MyTurn = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        //  SendLogMessage("Entrando a la sala");
        PhotonNetwork.LoadLevel("board");
    }
    public override void OnJoinedLobby()
    {

    }
    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("mainPageStudent");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo item in roomList)
        {
            if (item.RemovedFromList)
            {
                cachedRoomList.Remove(item.Name);
            }
            else
            {
                cachedRoomList[item.Name] = item;
            }

        }
        message.text = "Salas Activas: " + cachedRoomList.Count + "\n";

        foreach (KeyValuePair<string, RoomInfo> item in cachedRoomList)
        {
            message.text += item.Value.Name + " (" + item.Value.PlayerCount + "/" + item.Value.MaxPlayers + ")\n";

            // Debug.Log("")

        }
    }


}
