using System.Collections;
using Models;
using thisAPP.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI ELEMENTS")]
    public DialogueUi uiElements;
    public UIMediatorManager uIMediatorManager;

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

    public void NextSentece()
    {
        uiElements.SkipButton.interactable = false;
        if (currentDialog < Sentences.Length)
            StartCoroutine(IterateSentece());
        else
            uIMediatorManager.HideDialogue();

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

    //override 

}
