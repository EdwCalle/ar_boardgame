using TMPro;
using UnityEngine;

public class RoomItemManager : MonoBehaviour
{
    public TextMeshProUGUI roomName;    // Start is called before the first frame update

    void Start()
    {

    }
    public void JoinRoom()
    {
        GameObject.Find("MatchmakingCotroller").GetComponent<MatchmakingManager>().JoinRoomInList(roomName.text);
    }
}
