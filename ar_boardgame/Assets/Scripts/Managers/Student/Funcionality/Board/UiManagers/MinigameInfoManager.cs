using System.Threading.Tasks;
using thisAPP.UIElements;
using UnityEngine;

public class MinigameInfoManager : MonoBehaviour, IModalAnimator
{
    public MinigameInfo uiElements;
    public void SetTitlteMinigame()
    {
        uiElements.MinigameTitle.text = "Vamos a jugar:" + Singleton.GetMinigameTitle();
    }
    public async Task ShowModal(GameObject canvasModal)
    {
        SetTitlteMinigame();
        uiElements.ShowModal(canvasModal);
        await Task.Delay(1050);

    }
    public async Task HideModal(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
    }
}
