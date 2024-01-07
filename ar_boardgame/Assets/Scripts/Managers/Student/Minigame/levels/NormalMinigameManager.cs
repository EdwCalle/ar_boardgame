
using Models;
using TMPro;
using UnityEngine;
public class NormalMinigameManager : MonoBehaviour, IMinigame
{
    // public Modal modal;
    public GameObject description;
    public TextMeshProUGUI descriptionText;
    public GameObject image;
    public GameObject Audio;


    public void SetExtraInfo(string extraInfo)
    {
        switch (extraInfo)
        {
            case "image":
                //image.SetActive(true);
                //image.SetActive(true);
                break;
            case "description":
                //description.SetActive(true);
                descriptionText.text = Singleton.GetCurrentMinigame().ExtraInfo.Info;
                break;
        }
    }

    public void LoadLevel()
    {
        Singleton.GetCurrentMinigame().SelectionType = SelectionType.MultipleSelection;
        SetExtraInfo(Singleton.GetCurrentMinigame().ExtraInfo.Type);
    }
    public void StartLevel()
    {
        if (Singleton.GetCurrentMinigame().ExtraInfo.Type == "image")
        {
            image.SetActive(true);
            description.SetActive(false);
            // audio.SetActive(false);
        }
        if (Singleton.GetCurrentMinigame().ExtraInfo.Type == "description")
        {
            image.SetActive(false);
            description.SetActive(true);
            // audio.SetActive(false);
        }
        gameObject.SetActive(true);
    }

}