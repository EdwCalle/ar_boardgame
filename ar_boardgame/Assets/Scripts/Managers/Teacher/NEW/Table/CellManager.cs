using TMPro;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public void BuildCell(string text = "this is a test", float fontSize = 62.5f, float width = 100)
    {
        title.text = text;
        title.fontSize = fontSize;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 70f);
    }

}
