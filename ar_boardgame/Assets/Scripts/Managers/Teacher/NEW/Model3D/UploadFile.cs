

using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
#if UNITY_EDITOR

        string filePath = EditorUtility.OpenFilePanel("Select a file", "", "");
        if (!string.IsNullOrEmpty(filePath))
        {
            if (model.transform.childCount != 0)
                Destroy(model.transform.GetChild(0).gameObject);
            model.GetComponent<GLTFComponent>().GLTFUri = filePath;
            model.GetComponent<GLTFComponent>().Load();
            // Use the selected file path.
        }
#endif

    }
    public void AbrirImagen(RawImage rendererComponent)
    {
#if UNITY_EDITOR
        string filePath = EditorUtility.OpenFilePanel("Select a file", "", "");

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new(2, 2); // Create a new Texture2D

        // Load the image file data into the Texture2D
        if (texture.LoadImage(fileData))
        {
            if (rendererComponent != null)
            {
                // Apply the loaded texture to the Renderer component
                rendererComponent.GetComponent<RawImage>().texture = texture;
            }
            else
            {
                Debug.LogError("Renderer component not assigned!");
            }
        }
        else
        {
            Debug.LogError("Failed to load image as texture!");
        }

#endif

    }
    public void changeScene()
    {
        SceneManager.LoadScene("crudModels");
    }



}
