using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowModelManager : MonoBehaviour
{
    public TextMeshProUGUI modelName;
    public RawImage image;
    public void SetData(Model3D model3D, Texture2D texture2D)
    {
        modelName.text = model3D.Name;
        image.texture = texture2D;
    }
}
