using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityGLTF;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    // public 

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetCollider(Models.Collider collider)
    {
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = collider.Center;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = collider.Size;
    }
    public void SetIndex(int index)
    {
        transform.GetChild(1).gameObject.GetComponent<SelectableModel>().index = index;
    }
    public void SetModelUri(string url)
    {
        transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().GLTFUri = url;
    }
    public void SetActive(bool active)
    {
        transform.GetChild(1).gameObject.SetActive(active);
    }
    public void SetSolution(bool solution)
    {
        transform.GetChild(1).gameObject.GetComponent<SelectableModel>().IsSolution = solution;

    }
    public void ShowFeedback()
    {
        transform.GetChild(1).gameObject.GetComponent<SelectableModel>().SetCorrect();
    }
    public async Task<bool> LoadModel()
    {
        try
        {
            await transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().Load();
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
    public void ItemManagerBuilder(int index, Models.Collider collider, string url, bool solution, bool active)
    {
        SetIndex(index);
        SetCollider(collider);
        SetModelUri(url);
        SetSolution(solution);
        SetActive(active);

    }

}
