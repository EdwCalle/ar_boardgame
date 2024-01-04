using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
public class ConnectionServer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI username;
    // Start is called before the first frame update
    void Start()
    {


        Debug.Log("hola me llamo.....");
        //modalMessage.SetActive(true);


    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("multiplayer");
    }
    public void GoLobby()
    {
        if (username.text.Length >= 1)
        {
            PhotonNetwork.NickName = username.text;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("unido al lobby");

    //     modalMessage.SetActive(false);
    //     //  SceneManager.LoadScene("multiplayer");
    // }


}
