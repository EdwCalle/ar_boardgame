
using System;
using Photon.Pun;
using Photon.Realtime;

public class AutomaticConnection : MonoBehaviourPunCallbacks
{
    //public string scene;
    //  public TextMeshProUGUI LogMessage;
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = "";
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
        PhotonNetwork.NickName = "Player" + PhotonNetwork.CurrentRoom.PlayerCount;
        Singleton.instance.MyTurn = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        SendLogMessage("Entrando a la sala");
        PhotonNetwork.LoadLevel("board");

    }
    public override void OnLeftRoom()
    {

    }
    public void SendLogMessage(String message)
    {
        // if (LogMessage != null)
        // {
        //     LogMessage.text = $"Log:{message}";
        // }
    }



}
