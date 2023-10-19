
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityGLTF;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    //for level 1,2,3,5,7
    List<GameObject> items = new List<GameObject>();
    //for level 4,6
    //private Gameobject test;

    void Start()
    {

    }

    public void addModelUri(string url)
    {
        itemPrefab.transform.GetChild(0).gameObject.GetComponent<ImporterManager>().GLTFUri = url;
    }
    public void addCollider(Vector3 colliderCenter, Vector3 colliderSize)
    {
        itemPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = colliderCenter;
        itemPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = colliderSize;
    }
    public void addIndex(int index)
    {
        itemPrefab.transform.GetChild(1).gameObject.GetComponent<SelectableModel>().index = index;
    }
    public void addItemsContainer()
    {
        itemPrefab.transform.GetChild(1).gameObject.GetComponent<SelectableModel>().Configure(this);
    }
    public void unselectModel(int index)
    {
        items[index].transform.GetChild(1).gameObject.GetComponent<SelectableModel>().switchModelSelected();
    }
    public GameObject createItem(Vector3 Position)
    {
        itemPrefab.transform.GetChild(0).gameObject.GetComponent<ImporterManager>().LoadModel();
        GameObject instance = Instantiate(itemPrefab, Position, Quaternion.identity);
        return instance;
    }


    public void LoadMultipleModels()
    {
        int i = 0;
        foreach (Item item in Singleton.instance.currentMinigame.Models)
        {

            addModelUri(item.Url);
            addCollider(item.GetColliderCenter(), item.GetColliderSize());
            addItemsContainer();
            addIndex(i++);
            items.Add(createItem(item.GetPosition()));


        }
    }
    public void PrepareLevel()
    {
        SelectableModel.selection = SelectableModel.SelectionType.noSelection;
        // selection = SelectionType.OneSelection;
        LoadMultipleModels();
    }




}
