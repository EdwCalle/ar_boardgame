
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;

public class Models3DManager : MonoBehaviour
{
    public MinigameManager minigameManager;
    public GameObject enviroment;
    [SerializeField] private GameObject itemPrefab;

    private List<GameObject> models3D = new();


    public List<GameObject> Models3D { get => models3D; set => models3D = value; }

    //for level 4,6

    public void ActivateModels3D()
    {
        if (Singleton.GetCurrentMinigame().Level == 1 || Singleton.GetCurrentMinigame().Level == 3)
            transform.gameObject.SetActive(true);
        if (Singleton.GetCurrentMinigame().Level == 2)
        {
            transform.gameObject.SetActive(true);
            enviroment.SetActive(true);
        }
    }
    public void ActiveCurrentModel3D(int before, int current)
    {
        models3D[before].SetActive(false);
        models3D[current].SetActive(true);
    }
    public void FoundItem(int index)
    {
        minigameManager.searchMinigameManager.FoundItem(index);
    }
    public void ShowFeedback()
    {
        for (int i = 0; i < models3D.Count; i++)
        {
            models3D[i].GetComponent<Model3DManager>().ShowFeedback(i);
        }
    }
    public async Task LoadMultipleModels()
    {
        int i = 0;
        foreach (Model3D model3D in Singleton.GetCurrentMinigame().Models)
        {
            GameObject instance = Instantiate(itemPrefab, model3D.Position, Quaternion.identity, transform);

            instance.GetComponent<Model3DManager>().ItemManagerBuilder(index: i++, modelInBox: model3D.ModelInBox, colliderSize: model3D.ColliderScale, url: model3D.Url, solution: model3D.IsSolution, this);

            bool verify = await instance.GetComponent<Model3DManager>().LoadModel();

            if (!verify)
                print("something wrong happens, please make something with this");

            if (Singleton.GetCurrentMinigame().Level == 3)
            {
                instance.SetActive(false);
            }
            models3D.Add(instance);
        }

    }
    public async Task LoadLevel()
    {

        int level = Singleton.GetCurrentMinigame().Level;
        if (level == 1 || level == 2 || level == 3)
        {
            await LoadMultipleModels();
        }
    }

    // void OnDestroy()
    // {
    //     items = null;
    // }





}
