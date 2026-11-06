using thisAPP.UIElements;
using UnityEngine;
public class TurnInfoManager : MonoBehaviour
{
    public TurnUI uiElements;
    public GameManager gameManager;
    public void ShowTurn(GameObject canvasModal)
    {
        uiElements.ShowModal(canvasModal, "turn");
    }
    public void HideTurn(GameObject canvasModal)
    {
        uiElements.HideModal(canvasModal, "turn");
    }
    public void PrepareTurnInfo(string message)
    {
        uiElements.TitleTurn.text = message;
        ShowTurn(gameManager.canvasModal);
        uiElements.ImageSprite.sprite = uiElements.Avatars[Singleton.GetCurrentPlayerTurn()];
    }
}
