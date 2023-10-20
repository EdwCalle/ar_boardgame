using UnityEngine;
using TMPro;
using Models;
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
    //usado para referenciar el mediador
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
        HideExtraComponents();
        if (Singleton.instance.currentMinigame.Header == HeaderType.extraInfo) // if level 1 
        {
            SetExtraInfoFromSinglenton();
            SetTitleFromSinglenton();
        }
        if (Singleton.instance.currentMinigame.Header == HeaderType.normal) // if level 2,3,5,6,7,8,9(special)
        {
            SetTitleFromSinglenton();
        }
        if (Singleton.instance.currentMinigame.Header == HeaderType.questions) // if level 4
        {
            SetTitleFromSinglenton();
        }


        //este debe ser personalizado desde el mediator



    }



}
