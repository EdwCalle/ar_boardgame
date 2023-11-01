
using UnityEditor;
using UnityEngine;
using UnityGLTF;

public class UploadFile : MonoBehaviour
{
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AbrirArchivo()
    {
        string filePath = EditorUtility.OpenFilePanel("Select a file", "", "");
        if (!string.IsNullOrEmpty(filePath))
        {
            model.GetComponent<GLTFComponent>().GLTFUri = filePath;
            model.GetComponent<GLTFComponent>().Load();
            // Use the selected file path.
        }


    }

}
