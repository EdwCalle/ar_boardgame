using System.Collections.Generic;
using Models;
using UnityEngine;

public class ExtraOneManager : MonoBehaviour
{
    public GameObject itemPrefab;
    List<GameObject> items = new();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PrepareLevel()
    {
        foreach (Item model in Singleton.instance.currentMinigame.Models)
        {
            GameObject item = Instantiate(itemPrefab, transform);
            item.GetComponent<ItemFoundManager>().nameText.text = model.Name;
            //item.GetComponent<ItemFoundManager>().nameText.text = model.Name;
            items.Add(item);
        }
    }
    public void CheckItem(int index)
    {
        items[index].GetComponent<ItemFoundManager>().toggle.isOn = true;
    }

    public void ActiveListFound(bool value)
    {

        if (Singleton.instance.currentMinigame.ExtraComponent == ExtraComponentType.explore)
        {
            gameObject.SetActive(value);
        }


    }

}
