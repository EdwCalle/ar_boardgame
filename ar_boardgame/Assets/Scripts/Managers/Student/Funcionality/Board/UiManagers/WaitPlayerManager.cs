using System.Threading.Tasks;
using thisAPP.UIElements;
using UnityEngine;

public class WaitPlayerManager : MonoBehaviour, IModalAnimator
{
    [Header("UI ELEMENTS")]
    public WaitPlayerUi uiElements;
    //int PlayerReady = 0;
    public void SetMessage(string message)
    {
        uiElements.Message.text = message;
    }
    // public void WaitPlayers()
    // {
    //     PlayerReady++;
    // }
    public async Task ShowModal(GameObject canvasModal)
    {
        uiElements.ShowModal(canvasModal, "waitPlayer");
        await Task.Delay(2000);
    }
    public async Task HideModal(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal, "waitPlayer");
        await Task.Delay(2000);
    }



}
