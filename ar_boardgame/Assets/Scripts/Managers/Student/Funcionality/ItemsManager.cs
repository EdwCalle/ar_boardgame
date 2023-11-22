
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;
//using UnityGLTF;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    //for level 1,2,3,5,7
    public static List<GameObject> items;
    //for level 4,6
    // [SerializeField] private MediatorMinigame mediatorMinigame;

    void Start()
    {
        items = new List<GameObject>();
    }

    public GameObject createItem(Vector3 Position)
    {
        return Instantiate(itemPrefab, Position, Quaternion.identity);
    }
    public static void ActiveSelectableComponent(bool active)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<ItemManager>().SetActive(active); ;
        }
    }
    public static void ShowFeedback()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<ItemManager>().ShowFeedback();
        }
    }
    public async Task LoadMultipleModels()
    {
        int i = 0;
        foreach (Item item in Singleton.instance.currentMinigame.Models)
        {
            GameObject instance = createItem(item.Position);
            instance.transform.parent = transform;
            instance.GetComponent<ItemManager>().ItemManagerBuilder(i++,
                                                                      item.Collider,
                                                                      item.Url,
                                                                      item.IsSolution,
                                                                      false);

            bool verify = await instance.GetComponent<ItemManager>().LoadModel();
            if (!verify)
                print("something wrong happens, please make something with this");

            items.Add(instance);



        }
    }
    public async Task PrepareLevel()
    {
        if (Singleton.instance.currentMinigame.ExtraComponent == ExtraComponentType.normal)
            await LoadMultipleModels();
    }

    void OnDestroy()
    {
        items = null;
    }





}
