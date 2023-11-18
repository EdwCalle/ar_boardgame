using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomItemPrefab;
    public GameObject[] AllRooms;
    public TextMeshProUGUI message;

    private float skipTime = 2.5f;
    private float nextTime;


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // UpdateList(roomList);
        // if (Time.time >= nextTime)

        // {

        //     // print("localmente tengo esto:" + AllRooms.Length);
        //     for (int i = 0; i < AllRooms.Length; i++)
        //     {
        //         if (AllRooms[i] != null)
        //         {
        //             Destroy(AllRooms[i]);
        //         }
        //     }
        //     AllRooms = new GameObject[roomList.Count];
        //     //print("Onlinemente tengo esto:" + roomList.Count);

        //     for (int i = 0; i < roomList.Count; i++)
        //     {
        //         RoomInfo info = roomList[i];
        //         //   if(info.RemovedFromList)
        //         if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
        //         {
        //             //  print(" cumplio tengo esto:" + i);

        //             GameObject roomItem = Instantiate(RoomItemPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
        //             roomItem.GetComponent<RoomItemManager>().roomName.text = roomList[i].Name;
        //             AllRooms[i] = roomItem;
        //             //  print(roomList[i].Name);
        //         }
        //     }
        //     nextTime = Time.time + skipTime;
        // }

    }
    void UpdateList(List<RoomInfo> roomList)
    {
        message.text = "Salas Activas:\n";

        // Itera a través de la lista de salas y agrega información al objeto de texto
        foreach (RoomInfo room in roomList)
        {
            message.text += room.Name + " (" + room.PlayerCount + "/" + room.MaxPlayers + ")\n";
        }
    }
}
