using System;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityGLTF;

public class Model3DManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject modelCollider;
    public GameObject model;

    public void SetModelInBox(ModelInBox modelInBox)
    {
        model.transform.localScale = modelInBox.Scale;
        model.transform.parent = modelCollider.transform;
        // model.transform.localScale = new Vector3(1f / modelInBox.Scale.x, 1f / modelInBox.Scale.y, 1f / modelInBox.Scale.z);
        model.transform.localPosition = modelInBox.Position;



        // transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = collider.Center;
        // transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = collider.Size;
    }
    public void SetColliderScale(Vector3 colliderScale)
    {
        model.transform.parent = transform;
        modelCollider.transform.localScale = colliderScale;
    }
    public void SetIndex(int index)
    {
        modelCollider.GetComponent<SelectableModel>().index = index;
        //transform.GetChild(1).gameObject.GetComponent<SelectableModel>().index = index;
    }
    public void SetModelUri(string url)
    {
        model.GetComponent<GLTFComponent>().GLTFUri = url;
        // transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().GLTFUri = url;
    }
    // public void SetActive(bool active)
    // {
    //     transform.GetChild(1).gameObject.SetActive(active);
    // }
    public void SetSolution(bool solution)
    {
        modelCollider.GetComponent<SelectableModel>().IsSolution = solution;
        // transform.GetChild(1).gameObject.GetComponent<SelectableModel>().IsSolution = solution;

    }
    public void SetModels3DManager(Models3DManager models3DManager)
    {
        modelCollider.GetComponent<SelectableModel>().Models3DManager = models3DManager;
    }
    public void ShowFeedback(int index)
    {

        modelCollider.GetComponent<SelectableModel>().SetCorrect(index);


        //   transform.GetChild(1).gameObject.GetComponent<SelectableModel>().SetCorrect();
    }
    public async Task<bool> LoadModel()
    {
        try
        {
            await model.GetComponent<GLTFComponent>().Load();
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
    public void ItemManagerBuilder(int index, ModelInBox modelInBox, Vector3 colliderSize, string url, bool solution, Models3DManager models3DManager)
    {
        SetIndex(index);
        SetColliderScale(colliderSize);
        SetModelInBox(modelInBox);
        SetModelUri(url);
        SetSolution(solution);
        SetModels3DManager(models3DManager);
        // SetActive(active);
    }


}
