using TMPro;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public void BuildCell(string text = "this is a test", float fontSize = 62.5f)
    {
        title.text = text;
        title.fontSize = fontSize;
    }
}
