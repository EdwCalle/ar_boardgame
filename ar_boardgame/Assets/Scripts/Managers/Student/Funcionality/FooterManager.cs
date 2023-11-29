using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FooterManager : MonoBehaviour
{
    public Button finish;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActivateFinish(bool active)
    {
        finish.gameObject.SetActive(active);
    }
    public void ChangeScene()
    {
        Singleton.instance.CurrentRound++;
        SceneManager.LoadScene("board");
    }
    public void PrepareLevel()
    {
        finish.onClick.AddListener(ChangeScene);
        ActivateFinish(false);
    }
}
