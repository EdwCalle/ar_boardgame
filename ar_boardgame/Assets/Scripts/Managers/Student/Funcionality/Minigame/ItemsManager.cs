
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.U2D.IK;
//using UnityGLTF;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private MediatorMinigame mediatorMinigame;

    //for level 1,2,3,5,7
    public static List<GameObject> items;

    public GameObject objects;
    //for level 4,6
    // [SerializeField] private MediatorMinigame mediatorMinigame;

    void Start()
    {

        items = new List<GameObject>();
        transform.gameObject.SetActive(false);
    }

    public GameObject createItem(Vector3 Position)
    {
        return Instantiate(itemPrefab, Position, Quaternion.identity);
    }
    public void ActivateItems()
    {
        // for (int i = 0; i < items.Count; i++)
        // {
        //     items[i].GetComponent<ItemManager>().SetActive(active); ;
        // }
        transform.gameObject.SetActive(true);

        ExtraComponentType extraComponentType = Singleton.instance.currentMinigame.ExtraComponent;
        if (extraComponentType == ExtraComponentType.options)
        {

        }
        else
        {
            objects.SetActive(true);
        }
    }
    public void ActiveCurrentItem(int before, int current)
    {
        items[before].SetActive(false);
        items[current].SetActive(true);
    }

    public static void ShowFeedback()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<ItemManager>().ShowFeedback(i);
        }

    }
    public async Task LoadMultipleModels()
    {
        int i = 0;
        foreach (Item item in Singleton.instance.currentMinigame.Models)
        {
            GameObject instance = createItem(item.Position);
            instance.transform.GetChild(0).GetComponent<SelectableModel>().mediatorMinigame = mediatorMinigame;

            instance.transform.parent = transform;
            instance.GetComponent<ItemManager>().ItemManagerBuilder(i++,
                                                                      item.ModelInBox,
                                                                      item.ColliderScale,
                                                                      item.Url,
                                                                      item.IsSolution
                                                                      );

            bool verify = await instance.GetComponent<ItemManager>().LoadModel();

            if (!verify)
                print("something wrong happens, please make something with this");

            ExtraComponentType extraComponentType = Singleton.instance.currentMinigame.ExtraComponent;
            if (extraComponentType == ExtraComponentType.options)
            {
                instance.SetActive(false);
            }
            items.Add(instance);
        }

    }
    public async Task PrepareLevel()
    {
        ExtraComponentType extraComponentType = Singleton.instance.currentMinigame.ExtraComponent;
        if (extraComponentType == ExtraComponentType.normal || extraComponentType == ExtraComponentType.explore || extraComponentType == ExtraComponentType.options)
        {
            await LoadMultipleModels();

        }

    }

    void OnDestroy()
    {
        items = null;
    }





}
