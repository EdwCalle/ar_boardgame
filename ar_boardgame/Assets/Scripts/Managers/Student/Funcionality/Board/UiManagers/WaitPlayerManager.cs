using thisAPP.UIElements;
using UnityEngine;

public class WaitPlayerManager : MonoBehaviour
{
    [Header("UI ELEMENTS")]
    public WaitPlayerUi uiElements;
    public void SetMessage(string message)
    {
        uiElements.Message.text = message;
    }
}
