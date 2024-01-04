
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI nameCreatedRoom;
    public TextMeshProUGUI nameJoinedRoom;
    public TextMeshProUGUI message;
    public bool activateServer = false;
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    List<PlayerMatchmaking> playerMatchmakings = new();
    public PlayerMatchmaking playerMatchmakingPrefab;
    public Transform playerItemParent;
    public GameObject roomPanel;
    public GameObject startGameButton;
    public TextMeshProUGUI roomName;

    void Start()
    {

        if (activateServer)
        {
            if (PhotonNetwork.CountOfPlayers == 0)
                PhotonNetwork.NickName = "evder";
            else
                PhotonNetwork.NickName = "charly";

            // PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            PhotonNetwork.JoinLobby(customLobby);



    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }
    void UpdatePlayerList()
    {
        foreach (PlayerMatchmaking item in playerMatchmakings)
        {
            Destroy(item.gameObject);
        }
        playerMatchmakings.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerMatchmaking newPlayerItem = Instantiate(playerMatchmakingPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            if (player.Value == PhotonNetwork.LocalPlayer)
                newPlayerItem.ApplyLocalChanges();
            playerMatchmakings.Add(newPlayerItem);

        }

    }


    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom(nameCreatedRoom.text, new RoomOptions()
        {
            MaxPlayers = 2,
            IsVisible = true,
            IsOpen = true,
            BroadcastPropsChangeToAll = true
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
        // Singleton.instance.boardPersistent.MyTurn = PhotonNetwork.LocalPlayer.ActorNumber;
        roomName.text = "Nombre de la sala:" + PhotonNetwork.CurrentRoom.Name;
        //roomName.text = PhotonNetwork.LocalPlayer.ActorNumber + "";
        roomPanel.SetActive(true);
        //  SendLogMessage("Entrando a la sala");
        UpdatePlayerList();
        //PhotonNetwork.LoadLevel("board");
    }
    public override void OnJoinedLobby()
    {

    }
    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
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
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }
    public void OnClickStartGameButton()
    {
        int i = 0;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Hashtable playerProperties = player.Value.CustomProperties;
            playerProperties["turn"] = i++;
            player.Value.SetCustomProperties(playerProperties);
            //  print("odio esta porqueria");
        }
        PhotonNetwork.LoadLevel("board");
    }



}
