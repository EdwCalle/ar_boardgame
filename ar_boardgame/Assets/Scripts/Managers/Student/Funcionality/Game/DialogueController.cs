using System.Collections;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI chat;
    public Button skipButton;
    public Dialogue[] dialogues;

    public GameObject[] Characters;
    public GameObject[] CharactersName;
    public int currentDialog = 0;
    [Range(0, 1)]
    public float timeBetweenChar = 0.2f;
    [Range(0, 10)]
    public float timeBetweenSentence = 2f;
    [Range(0, 1)]
    public float opacityCharacter = 0.5f;


    int focusedCharacter = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDialogue()
    {
        StartCoroutine(IterateSentece());
    }

    public void NextSentece()
    {
        skipButton.interactable = false;
        if (currentDialog < dialogues.Length)
            StartCoroutine(IterateSentece());
    }

    IEnumerator IterateSentece()
    {
        FocusCharacter(focusedCharacter, false);
        FocusCharacter(dialogues[currentDialog].NumberCharacter, true);
        focusedCharacter = dialogues[currentDialog].NumberCharacter;
        chat.text = "";
        foreach (char item in dialogues[currentDialog].Sentence.ToCharArray())
        {
            chat.text += item;
            yield return new WaitForSeconds(timeBetweenChar);
        }
        // yield return new WaitForSeconds(timeBetweenSentence);
        currentDialog++;
        skipButton.interactable = true;
        // chat.text = "";

    }
    void FocusCharacter(int value, bool focus)
    {

        Color fade = Color.white;
        if (focus)
            fade.a = 1f;
        else
            fade.a = opacityCharacter;
        Characters[value].GetComponent<RawImage>().color = fade;
        CharactersName[value].SetActive(focus);
    }

}
