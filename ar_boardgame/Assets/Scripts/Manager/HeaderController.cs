using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
public class HeaderManager : MonoBehaviour
{
    [SerializeField] private MediatorMinigame mediator;
    [SerializeField] private TextMeshProUGUI title;
    //level 1:
    [SerializeField] private GameObject image; // if type  is image
    [SerializeField] private GameObject description;
    [SerializeField] private TextMeshProUGUI descriptionText;
    /* if type  is description
                                        public AudioClip audio;// if type is audio
                                        public button audio;//  if type is audio*/
    //level 4:
    [SerializeField] private GameObject numberQuestion;
    [SerializeField] private TextMeshProUGUI numberQuestionText;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Configure(MediatorMinigame mediator)
    {
        this.mediator = mediator;
    }

    public void SetExtraInfoFromSinglenton()
    {
        SetExtraInfo(Singleton.instance.currentMinigame.ExtraInfo.Type, Singleton.instance.currentMinigame.ExtraInfo.Info);
    }
    public void SetTitleFromSinglenton()
    {
        SetTitle(Singleton.instance.currentMinigame.Title);
    }

    public void SetExtraInfo(string typeInfo, string info)
    {
        switch (typeInfo)
        {
            case "image":

                image.SetActive(true);
                //image.SetActive(true);
                break;
            case "description":
                description.SetActive(true);
                descriptionText.text = info;
                break;
        }
    }
    public void SetTitle(string title)
    {
        this.title.text = title;
    }
    public void SetNumberQuestion(int numberQuestion, int max)
    {
        this.numberQuestionText.text = $"{numberQuestion}/{max}";
    }

    public void HideExtraComponents()
    {
        image.SetActive(false);
        description.SetActive(false);
        numberQuestion.SetActive(false);
    }

    public void PrepareLevel()
    {
        //este debe ser personalizado desde el mediator
        HideExtraComponents();
        SetExtraInfoFromSinglenton();
        SetTitleFromSinglenton();
    }



}
