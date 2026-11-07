using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageStudentManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI username;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("selectplayer");
    }
    public void GoMultiplayer()
    {
        if (username.text.Length >= 1)
        {
            Singleton.SetPlayerName(username.text.Replace("\u200B", "").Trim());
            SceneManager.LoadScene("multiplayer");
        }
        else
        {
            Debug.Log("Username is empty");
        }
    }
}
