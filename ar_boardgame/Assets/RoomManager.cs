using Photon.Pun;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI nameCreatedRoom;
    public TextMeshProUGUI nameJoinedRoom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(nameCreatedRoom.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.CreateRoom(nameJoinedRoom.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("board");
    }
}
