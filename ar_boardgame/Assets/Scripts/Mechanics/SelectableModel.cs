using UnityEngine;
using Models;
public class SelectableModel : MonoBehaviour
{

    private Item item;

    public bool isSolution;
    public int index;
    public static int lastSelectedItem = -1;

    //public static SelectionType selection;
    void Start()
    {
        item = new Item();
        //itemm
        //selection = SimulationMode.s;

    }

    void OnMouseDown()
    {

        SelectionType selection = Singleton.instance.currentMinigame.Selection;
        //print("index:" + index + " last index:" + lastSelectedItem + " item selected:" + selection);
        // if the selection type is one selection and  the current item selected is the same that the last selected
        if (selection == SelectionType.OneSelection && lastSelectedItem == index)
        {
            switchModelSelected();
            lastSelectedItem = -1;
        }
        // if the selection type is one selection and  there is no item selected
        else if (selection == SelectionType.OneSelection && lastSelectedItem == -1)
        {
            switchModelSelected();
            lastSelectedItem = index;
        }

        // if the selection type is one selection and  there is another selected
        else if (selection == SelectionType.OneSelection && lastSelectedItem != -1)
        {

            unselectModel(lastSelectedItem);
            switchModelSelected();
            lastSelectedItem = index;

        }
        if (selection == SelectionType.MultipleSelection)
            // if the selection is Multyply selection 
            switchModelSelected();
    }
    void Update()
    {
        // if (timer.GetComponent<CountdownTimer>().isCountingDown)
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //         RaycastHit hit;

        //         if (Physics.Raycast(ray, out hit))
        //         {
        //             if (hit.collider.gameObject == checkbox)
        //             {
        //                 // This object was clicked.
        //                 // Put your click logic here.
        //                 print("selected model" + item.Name);
        //                 selectModel();
        //             }
        //         }
        //     }
        // }
        // else
        // {
        //     print("go 1");
        //     if (!stop)
        //     {
        //         print("go 2");
        //         if (timer.GetComponent<CountdownTimer>().showResults)
        //         {
        //             print("go 3");
        //             setCorrect();
        //         }
        //     }


        // }

    }
    public void setCorrect()
    {
        if (item.IsSolution == item.IsSelected)
        {

            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }

    }
    public void switchModelSelected()
    {

        if (!item.IsSelected)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            item.IsSelected = true;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            item.IsSelected = false;
        }

    }
    public void unselectModel(int index)
    {
        ItemsManager.items[index].transform.GetChild(1).gameObject.GetComponent<SelectableModel>().switchModelSelected();
    }

}
