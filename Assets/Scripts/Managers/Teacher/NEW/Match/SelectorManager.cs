
using System.Collections.Generic;
using Models;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public Transform allItemsContainer;
    public Transform selectedItemsContainer;

    public GameObject roundListPrefab; //prefab

    private List<Round> selectedRounds = new();

    public List<Round> SelectedRounds { get => selectedRounds; set => selectedRounds = value; }

    // Start is called before the first frame update

    public void CreateList(List<Round> leftRounds, List<Round> rightRounds)
    {
        selectedRounds = rightRounds;
        foreach (Round item in leftRounds)
        {
            CreateElement(item, false);
        }
        foreach (Round item in rightRounds)
        {
            CreateElement(item, true);
        }
    }
    void CreateElement(Round item, bool direction)
    {
        GameObject instance = Instantiate(roundListPrefab, allItemsContainer);
        instance.GetComponent<RoundItemManager>().PrepareData(item, this, direction);
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
        selectedRounds.Clear();
    }



}
