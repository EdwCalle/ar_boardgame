using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            print(roomList[i].Name);
        }

    }
}
