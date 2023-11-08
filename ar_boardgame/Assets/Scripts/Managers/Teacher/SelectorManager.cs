
using System.Collections.Generic;
using Models;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public Transform allItemsContainer;
    public Transform selectedItemsContainer;

    public GameObject elementSelector; //prefab
    // Start is called before the first frame update

    public List<GameObject> items;
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void CreateList(List<Round> leftRounds, List<Round> rightRounds, List<Round> selectedRounds)
    {
        foreach (Models.Round item in leftRounds)
        {
            CreateElement(item, selectedRounds, false);
        }
        foreach (Models.Round item in rightRounds)
        {
            CreateElement(item, selectedRounds, true);
        }
    }
    void CreateElement(Round item, List<Round> selectedRounds, bool direction)
    {

        GameObject gameObject = Instantiate(elementSelector);
        gameObject.GetComponent<ElementListManager>().PrepareData(item, allItemsContainer, selectedItemsContainer, selectedRounds);
        if (direction)
            gameObject.GetComponent<ElementListManager>().SetRight();
        else
            gameObject.GetComponent<ElementListManager>().SetLeft();
        //  items.Add(gameObject);

    }
    public void ClearSelector()
    {
        for (int i = 0; i < allItemsContainer.childCount; i++)
        {
            Destroy(allItemsContainer.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < selectedItemsContainer.childCount; i++)
        {
            Destroy(selectedItemsContainer.transform.GetChild(i).gameObject);
        }
    }



}
