using Api;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public Transform allItemsContainer;
    public Transform SelectedItemsContainer;

    public GameObject elementSelector; //prefab
    // Start is called before the first frame update
    void Start()
    {
        CrudGeneralGame crud = new CrudGeneralGame();

        foreach (Models.Round item in crud.GetRounds())
        {

            elementSelector.GetComponent<ElementListManager>().allItemsContainer = allItemsContainer;
            elementSelector.GetComponent<ElementListManager>().SelectedItemsContainer = SelectedItemsContainer;
            elementSelector.GetComponent<ElementListManager>().SetTitle(item.Title);
            Instantiate(elementSelector);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

}
