using thisAPP.UIElements;
using UnityEngine;

public class ArrowsManager : MonoBehaviour
{
    [Header("UI ELEMENTS")]
    public Arrows uiElements;
    public void ActivateArrows(string directionOne, string directionTwo)
    {
        SetActiveArrow(uiElements.LeftButton, directionOne, directionTwo, "left");
        SetActiveArrow(uiElements.RightButton, directionOne, directionTwo, "right");
        SetActiveArrow(uiElements.UpButton, directionOne, directionTwo, "up");
        SetActiveArrow(uiElements.DownButton, directionOne, directionTwo, "down");
    }
    void SetActiveArrow(GameObject arrow, string directionOne, string directionTwo, string direction)
    {
        if (directionOne == direction || directionTwo == direction)
            arrow.SetActive(true);
        else
            arrow.SetActive(false);
    }

}
