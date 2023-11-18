
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI nameCreatedRoom;
    public TextMeshProUGUI nameJoinedRoom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(nameCreatedRoom.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 4, IsVisible = true, IsOpen = true }, TypedLobby.Default);
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
        PhotonNetwork.LoadLevel("board");
    }
    public void RefreshRoomList()
    {
        //  PhotonNetwork.coun
    }

    // public override void OnRoomListUpdate(List<RoomInfo> roomList)
    // {
    //     base.OnRoomListUpdate(roomList);
    // }


}
