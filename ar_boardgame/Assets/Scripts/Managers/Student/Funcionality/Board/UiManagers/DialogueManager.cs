using System.Collections;
using System.Threading.Tasks;
using Models;
using thisAPP.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour, IModalAnimator
{
    // Start is called before the first frame update
    [Header("UI ELEMENTS")]
    public DialogueUi uiElements;
    //public UIMediatorManager uIMediatorManager;
    public GameManager gameManager;

    [Header("Internal Funcionality")]
    int currentDialog = 0;
    [Range(0, 1)]
    public float timeBetweenChar = 0.2f;
    [Range(0, 10)]
    public float timeBetweenSentence = 2f;
    [Range(0, 1)]
    public float opacityCharacter = 0.5f;
    int focusedCharacter = 1;
    Sentence[] sentences;
    public Sentence[] Sentences { get => sentences; set => sentences = value; }
    void Start()
    {

    }
    public void StartDialogue()
    {
        StartCoroutine(IterateSentece());
    }

    public async void NextSentece()
    {
        uiElements.SkipButton.interactable = false;
        if (currentDialog < Sentences.Length)
            StartCoroutine(IterateSentece());
        else
        {
            await HideModal(gameManager.canvasModal);
        }
    }

    IEnumerator IterateSentece()
    {

        FocusCharacter(focusedCharacter, false);
        FocusCharacter(Sentences[currentDialog].NumberCharacter, true);
        focusedCharacter = Sentences[currentDialog].NumberCharacter;
        uiElements.Chat.text = "";
        foreach (char item in Sentences[currentDialog].Line.ToCharArray())
        {
            uiElements.Chat.text += item;
            yield return new WaitForSeconds(timeBetweenChar);
        }
        // yield return new WaitForSeconds(timeBetweenSentence);
        currentDialog++;
        uiElements.SkipButton.interactable = true;
        // chat.text = "";

    }
    void FocusCharacter(int value, bool focus)
    {
        Color fade = Color.white;
        if (focus)
            fade.a = 1f;
        else
            fade.a = opacityCharacter;
        uiElements.Characters[value].GetComponent<RawImage>().color = fade;
        uiElements.CharactersName[value].SetActive(focus);
    }
    public void SetDialogue()
    {
        Sentences = Singleton.GetSentences();
    }

    public async Task ShowModal(GameObject canvasModal)
    {
        uiElements.ShowModal(canvasModal);
        await Task.Delay(2000);
        SetDialogue();
        NextSentece();
    }
    public async Task HideModal(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
        gameManager.waitPlayerManager.SetMessage("sincronizando tablero, esperando al resto de jugadores");
        gameManager.waitPlayerManager.uiElements.WaitBoard.SetActive(false);
        gameManager.waitPlayerManager.uiElements.WaitPlayer.SetActive(true);
        await gameManager.WaitPlayer("ShowDiceRPC");
    }

    //override 


}
