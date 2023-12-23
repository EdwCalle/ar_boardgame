using thisAPP.UIElements;
using UnityEngine;

public class MinigameInfoManager : MonoBehaviour
{
    public MinigameInfo uiElements;
    // Start is called before the first frame update

    public void SetTitlteMinigame()
    {
        uiElements.MinigameTitle.text = "Vamos a jugar:" + Singleton.GetMinigameTitle();
    }
}
