using System.Threading.Tasks;
using thisAPP.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class ArrowsManager : MonoBehaviour, IModalAnimator
{
    [Header("UI ELEMENTS")]
    public ArrowsUI uiElements;
    public GameManager gameManager;
    public void ActivateArrows(string directionOne, string directionTwo)
    {

        SetActiveArrow(uiElements.LeftButton, directionOne, directionTwo, "left");
        SetActiveArrow(uiElements.RightButton, directionOne, directionTwo, "right");
        SetActiveArrow(uiElements.UpButton, directionOne, directionTwo, "up");
        SetActiveArrow(uiElements.DownButton, directionOne, directionTwo, "down");
        if (Singleton.IsMyTurn())
        {
            gameManager.turnInfoManager.PrepareTurnInfo("Elije el camino por el cual quieres avanzar");
            //  uiElements.TitleTurn.text =
            //sprite player
        }
        else
        {
            gameManager.turnInfoManager.PrepareTurnInfo(message: $"Espera un momento, el jugador{Singleton.GetCurrentPlayerTurn()} esta seleccionando el camino");
            //uiElements.TitleTurn.text = $"Espera un momento, el jugador{Singleton.GetCurrentPlayerTurn()} esta seleccionando el camino";
            //sprite player
        }

    }
    void SetActiveArrow(GameObject arrow, string directionOne, string directionTwo, string direction)
    {
        if (Singleton.IsMyTurn())
            arrow.GetComponent<Button>().interactable = true;
        else
            arrow.GetComponent<Button>().interactable = false;
        if (directionOne == direction || directionTwo == direction)
            arrow.SetActive(true);
        else
            arrow.SetActive(false);

    }
    public void ChangeDirection(string direction)
    {
        gameManager.ChangeDirection(direction);
    }
    public async Task ShowModal(GameObject canvasModal)
    {

        uiElements.ShowModal(canvasModal);
        await Task.Delay(1050);
    }
    public async Task HideModal(GameObject canvasModal)
    {
        gameManager.turnInfoManager.HideTurn(canvasModal);
        uiElements.HideModal(canvasModal);
        await Task.Delay(1050);
    }


}
