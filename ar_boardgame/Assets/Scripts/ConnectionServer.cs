using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectionServer : MonoBehaviourPunCallbacks
{
    public GameObject modalMessage;
    // Start is called before the first frame update
    void Start()
    {
        //modalMessage.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("unido al lobby(room)");

        modalMessage.SetActive(false);
        // SceneManager.LoadScene("multiplayer");
    }
    public ConnectionServer()
    {
        /// show loading page
    }


    // Update is called once per frame
    void Update()
    {

    }
}
