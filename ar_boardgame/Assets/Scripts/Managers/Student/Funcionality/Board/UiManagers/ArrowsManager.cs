using thisAPP.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class ArrowsManager : MonoBehaviour
{
    [Header("UI ELEMENTS")]
    public ArrowsUI uiElements;
    public void ActivateArrows(string directionOne, string directionTwo)
    {

        SetActiveArrow(uiElements.LeftButton, directionOne, directionTwo, "left");
        SetActiveArrow(uiElements.RightButton, directionOne, directionTwo, "right");
        SetActiveArrow(uiElements.UpButton, directionOne, directionTwo, "up");
        SetActiveArrow(uiElements.DownButton, directionOne, directionTwo, "down");
        if (Singleton.IsMyTurn())
        {
            uiElements.TitleTurn.text = "Elije el camino por el cual quieres ir";
            //sprite player
        }
        else
        {
            uiElements.TitleTurn.text = $"Espera un momento, el jugador{Singleton.GetCurrentPlayerTurn()} esta seleccionando el camino";
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

}
