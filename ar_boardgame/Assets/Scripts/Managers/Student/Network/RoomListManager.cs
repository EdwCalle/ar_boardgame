using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    public GameObject RoomItemPrefab;
    public GameObject[] AllRooms;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        print("localmente tengo esto:" + AllRooms.Length);
        for (int i = 0; i < AllRooms.Length; i++)
        {
            if (AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }
        AllRooms = new GameObject[roomList.Count];
        print("Onlinemente tengo esto:" + roomList.Count);

        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            //   if(info.RemovedFromList)
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                print(" cumplio tengo esto:" + i);

                GameObject roomItem = Instantiate(RoomItemPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                roomItem.GetComponent<RoomItemManager>().roomName.text = roomList[i].Name;
                AllRooms[i] = roomItem;
                print(roomList[i].Name);
            }
        }


    }
}
