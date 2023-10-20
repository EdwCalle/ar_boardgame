
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityGLTF;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    //for level 1,2,3,5,7
    public static List<GameObject> items = new List<GameObject>();
    //for level 4,6
    //private Gameobject test;

    void Start()
    {

    }

    public GameObject createItem(Vector3 Position)
    {
        return Instantiate(itemPrefab, Position, Quaternion.identity);
    }


    public async Task LoadMultipleModels()
    {
        int i = 0;
        foreach (Item item in Singleton.instance.currentMinigame.Models)
        {
            GameObject instance = createItem(item.Position);
            instance.GetComponent<ItemManager>().ItemManagerBuilder(i++,
                                                                      item.Collider,
                                                                      item.Url);

            bool verify = await instance.GetComponent<ItemManager>().LoadModel();
            if (!verify)
                print("something wrong happens, please make something with this");

            items.Add(instance);



        }
    }
    public async Task PrepareLevel()
    {
        SelectableModel.selection = SelectableModel.SelectionType.MultipleSelection;
        // selection = SelectionType.OneSelection;
        await LoadMultipleModels();
    }




}
