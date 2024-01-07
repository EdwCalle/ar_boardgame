using TMPro;
using UnityEngine;


public class LoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI message;

    public GameObject titleMinigame;
    [SerializeField] private TextMeshProUGUI titleMinigameText;

    public void StartAnimation(string message)
    {
        this.message.text = message;
        gameObject.SetActive(true);

    }
    public void StopAnimation()
    {
        gameObject.SetActive(false);
    }
    public void SetTitle()
    {
        SetTitle(Singleton.GetCurrentMinigame().Title);
    }
    public void SetTitle(string title)
    {

        titleMinigameText.text = title;
    }
    public void LoadLevel()
    {
        SetTitle();
    }
    public void StartLevel()
    {
        titleMinigame.SetActive(true);
    }

}
