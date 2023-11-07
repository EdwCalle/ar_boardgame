
using System.Collections.Generic;
using Models;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public Transform allItemsContainer;
    public Transform selectedItemsContainer;

    public GameObject elementSelector; //prefab
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void CreateList(List<Round> allRounds)//, List<Round> selectedRounds)
    {
        foreach (Models.Round item in allRounds)
        {
            CreateElement(item);//, this.selectedRounds);
        }
    }
    void CreateElement(Round item)//, List<Round> selectedRounds)
    {
        elementSelector.GetComponent<ElementListManager>().PrepareData(item, allItemsContainer, selectedItemsContainer);//, selectedRounds);

        Instantiate(elementSelector);

    }


}
