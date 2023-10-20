
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public GameObject buttons;
    public GameObject dialogFinish;
    // Start is called before the first frame update
    void Awake()
    {
        HideAll();
    }
    public void HideAll()
    {
        buttons.SetActive(false);
        dialogFinish.SetActive(false);
    }
    public void SetTextButtons()
    {
        text1.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame1.Title;
        text2.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame2.Title;
        text3.text = Singleton.instance.currentGeneralGame.Rounds[Singleton.instance.currentRound].Minigame3.Title;
    }

    public void ShowButtons()
    {
        HideAll();
        buttons.SetActive(true);
    }
    public void ShowDialogFinish()
    {
        HideAll();
        dialogFinish.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
