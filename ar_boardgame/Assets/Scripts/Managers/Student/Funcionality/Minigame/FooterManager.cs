using JetBrains.Annotations;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FooterManager : MonoBehaviour
{
    [System.Serializable]
    public class Texting
    {
        [SerializeField] private GameObject container;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI label2;

        // [SerializeField] private TextMeshProUGUI testing;
        public GameObject Container { get => container; set => container = value; }
        public TextMeshProUGUI Label { get => label; set => label = value; }
        public TextMeshProUGUI Label2 { get => label2; set => label2 = value; }
        //    public TextMeshProUGUI Testing { get => testing; set => testing = value; }
    }
    public Button finish;

    public Texting finalMessage;
    public Texting Result;
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
    public void ShowResults()
    {

        Result.Container.SetActive(true);
        Score currentMinigameScore = Score.GetMinigameResults(Singleton.instance.currentMinigame);
        Result.Label.text = $"Ganaste {currentMinigameScore.Stars}";
        Result.Label2.text = $"de {Singleton.instance.currentMinigame.Models.Count} posibles";


    }
}
