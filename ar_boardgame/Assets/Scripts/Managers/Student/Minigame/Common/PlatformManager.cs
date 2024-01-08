using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public MinigameManager minigameManager;
    public bool enableAR = false;
    public GameObject arSession;
    public GameObject arOrigin;
    public GameObject PlayerPrefab;
    public GameObject walls;
    public void SwitchPlatform()
    {


        //if(Application.platform==RuntimePlatform.WindowsEditor)
#if UNITY_EDITOR || UNITY_STANDALONE
        if (enableAR)
        {
            arSession.SetActive(true);
            arOrigin.SetActive(true);
            minigameManager.countdownTimer.IsAR = true;
        }
        else
        {
            walls.SetActive(true);
            Instantiate(PlayerPrefab);
            minigameManager.countdownTimer.IsAR = false;

        }
#elif UNITY_ANDROID || UNITY_UNITY_IOS
     arSession.SetActive(true);
       arOrigin.SetActive(true);
         minigameManager.countdownTimer.IsAR = true;
      //  Instantiate(arExperiencePrefab);

#endif
    }
}