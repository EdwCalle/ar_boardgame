using System.Collections.Generic;
using Models;
using UnityEngine;

public class SearchMinigameManager : MonoBehaviour, IMinigame
{
    public GameObject models3DFounds;
    public GameObject itemPrefab;
    List<GameObject> models3D = new();
    public void LoadLevel()
    {
        Singleton.GetCurrentMinigame().SelectionType = SelectionType.OneTimeSelection;
        foreach (Model3D model in Singleton.GetCurrentMinigame().Models)
        {
            GameObject item = Instantiate(itemPrefab, models3DFounds.transform);
            item.GetComponent<ItemFoundManager>().nameText.text = model.Name;
            models3D.Add(item);
            print("me imprimi:" + item.name);
        }
    }

    public void StartLevel()
    {
        gameObject.SetActive(true);
    }
    public void FoundItem(int index)
    {
        models3D[index].GetComponent<ItemFoundManager>().toggle.isOn = true;
    }



}