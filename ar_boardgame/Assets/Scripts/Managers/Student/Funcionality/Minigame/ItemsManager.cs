
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private MediatorMinigame mediatorMinigame;
    [SerializeField] private GameObject itemPrefab;

    //for level 1,2,3,5,7
    private List<GameObject> items = new();

    public GameObject modalObjects;

    public List<GameObject> Items { get => items; set => items = value; }

    //for level 4,6

    void Start()
    {


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
        if (extraComponentType == ExtraComponentType.options || extraComponentType == ExtraComponentType.normal)
        {

        }
        else
        {
            modalObjects.SetActive(true);
        }
    }
    public void ActiveCurrentItem(int before, int current)
    {
        items[before].SetActive(false);
        items[current].SetActive(true);
    }

    public void ShowFeedback()
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

    // void OnDestroy()
    // {
    //     items = null;
    // }





}
