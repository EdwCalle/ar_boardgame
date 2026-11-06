using Models;
using TMPro;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public MinigameManager minigameManager;
    public TextMeshProUGUI label1;
    public TextMeshProUGUI label2;


    public void ShowResults()
    {
        if (Singleton.GetCurrentMinigame().Level == 1)
        {
            minigameManager.models3DManager.ShowFeedback();
        }
        if (Singleton.GetCurrentMinigame().Level == 2)
        {
            //minigameManager.models3DManager.ShowFeedback();
        }
        if (Singleton.GetCurrentMinigame().Level == 3)
        {
            minigameManager.quizMinigameManager.ShowFeedback();
        }
        Score myPartialScore = ScoreManager.GetMinigameResults(Singleton.GetCurrentMinigame());
        minigameManager.SetScore(myPartialScore);
        label1.text = $"Ganaste {myPartialScore.Stars}";
        label2.text = $"de {Singleton.GetCurrentMinigame().Models.Count} posibles";
        // ScoreManager.SetPartialScore(PhotonNetwork.LocalPlayer, myPartialScore);
        gameObject.SetActive(true);

    }


}