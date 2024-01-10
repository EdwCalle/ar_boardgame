
using System;
using System.Threading.Tasks;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityGLTF;

public class Model3DTManager : MonoBehaviour
{
    public GameObject modelCollider;
    public GameObject modelGLTF;
    public GameObject resume;
    public TextMeshPro[] title = new TextMeshPro[2];
    public TextMeshPro[] description = new TextMeshPro[2];
    public TextMeshPro[] copyright = new TextMeshPro[2];


    public UnityAction<int> selectModel;
    public int code = -1;


    public void SetModelInBox(ModelInBox modelInBox)
    {
        modelGLTF.transform.localScale = modelInBox.Scale;
        modelGLTF.transform.parent = modelCollider.transform;

        modelGLTF.transform.localPosition = modelInBox.Position;
    }
    public void SetColliderScale(Vector3 colliderScale)
    {
        //  model.transform.parent = transform;
        modelCollider.transform.localScale = colliderScale;
        float x = (colliderScale.x / 2) + 0.1f; //(0.2-0,0.117/2)=0.0585-0.2=0.1415-0.1=0.0415+0.0585=1;
        resume.transform.position = new Vector3(x, resume.transform.position.y, resume.transform.position.z);
    }
    public void SetIndex(int index)
    {
        code = index;

    }
    public void SetModelUri(string url)
    {
        modelGLTF.GetComponent<GLTFComponent>().GLTFUri = url;
    }


    public async Task<bool> LoadModel()
    {
        try
        {
            await modelGLTF.GetComponent<GLTFComponent>().Load();
            //model.
            //  await transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().Load();
            return true;
        }
        catch (Exception a)
        {
            // I think this can work perfecly, add retry time to load objects
            print("ERRORES:" + a.ToString());
            return false;
            //throw;
        }

    }
    public void ItemManagerBuilder(int index, Model3D model3D, ModelInBox modelInBox, Vector3 colliderSize, string url, UnityAction<int> unityAction)
    {
        SetIndex(index);
        SetColliderScale(colliderSize);
        SetModelInBox(modelInBox);
        SetModelUri(url);
        selectModel = unityAction;

        title[0].text = model3D.Name;
        title[1].text = model3D.Name;
        description[0].text = model3D.Description;
        description[1].text = model3D.Description;
        copyright[0].text = model3D.Copyright;
        copyright[1].text = model3D.Copyright;
        // SetSolution(solution);
        // SetModels3DManager(models3DManager);
        // SetActive(active);
    }
    void Update()
    {
        // Check for mouse button down
        if (Input.GetMouseButtonDown(0))  // 0 represents the left mouse button
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hits a specific GameObject
            if (hit.collider.gameObject == modelCollider.gameObject)
            {
                // This code will be executed when the user clicks on the specific GameObject
                Debug.Log("Mouse Clicked on " + gameObject.name);
                selectModel.Invoke(code);
                // Add your custom logic here
                // For example, you can change the color, play a sound, or perform any other action.
            }
        }
    }
    // void OnMouseDown()
    // {
    //     selectModel.Invoke(index);
    // }
    public void SelectModel()
    {
        Color currentColor = modelCollider.GetComponent<Renderer>().material.color;
        //  currentColor.a = Mathf.Clamp01(0f);
        currentColor.a = Mathf.Clamp01(0.3960784f);
        modelCollider.GetComponent<Renderer>().material.color = currentColor;
        // Modify the alpha channel and set it back to the material
    }
    public void UnselectModel()
    {
        Color currentColor = modelCollider.GetComponent<Renderer>().material.color;
        //  currentColor.a = Mathf.Clamp01(0f);
        currentColor.a = Mathf.Clamp01(0f);
        modelCollider.GetComponent<Renderer>().material.color = currentColor;
        // Modify the alpha channel and set it back to the material
    }
}
