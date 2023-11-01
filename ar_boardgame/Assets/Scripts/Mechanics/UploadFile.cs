
using UnityEditor;
using UnityEngine;
using UnityGLTF;

public class UploadFile : MonoBehaviour
{
    public GameObject Prefab;
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
        string filePath = EditorUtility.OpenFilePanel("Select a file", "", "glb|gltf");
        if (!string.IsNullOrEmpty(filePath))
        {
            Prefab.GetComponent<GLTFComponent>().GLTFUri = filePath;
            Prefab.GetComponent<GLTFComponent>().Load();
            // Use the selected file path.
        }


    }

}
