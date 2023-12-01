using UnityEngine;
using Models;
public class SelectableModel : MonoBehaviour
{


    private bool isSelected = false;
    private bool isSolution;
    public int index;
    public static int lastSelectedItem = -1;

    public bool IsSolution { get => isSolution; set => isSolution = value; }

    //public static SelectionType selection;
    void Start()
    {
    }
    void OnMouseDown()
    {
        // if the mode time is interaction just interact
        if (Singleton.instance.currentMinigame.TimerState == TimerState.resolve)
        {
            SelectionType selection = Singleton.instance.currentMinigame.Selection;
            //print("index:" + index + " last index:" + lastSelectedItem + " item selected:" + selection);
            // if the selection type is one selection and  the current item selected is the same that the last selected
            if (selection == SelectionType.OneSelection && lastSelectedItem == index)
            {
                SwitchModelSelected();
                lastSelectedItem = -1;
            }
            // if the selection type is one selection and  there is no item selected
            else if (selection == SelectionType.OneSelection && lastSelectedItem == -1)
            {
                SwitchModelSelected();
                lastSelectedItem = index;
            }

            // if the selection type is one selection and  there is another selected
            else if (selection == SelectionType.OneSelection && lastSelectedItem != -1)
            {

                UnselectModel(lastSelectedItem);
                SwitchModelSelected();
                lastSelectedItem = index;

            }
            if (selection == SelectionType.MultipleSelection)
                // if the selection is Multyply selection 
                SwitchModelSelected();
        }
        // if (!GetComponent<RotateModel>().isRotating)
        //     SwitchModelSelected();

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
        //                 print("selected model" + Name);
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
    public void SetCorrect()
    {
        if (IsSolution == isSelected)
        {

            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }

    }
    public void SwitchModelSelected()
    {

        // if (!isSelected)
        // {
        //     GetComponent<Renderer>().material.color = Color.cyan;
        //     // GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
        //     isSelected = true;
        // }
        // else
        // {
        //     GetComponent<Renderer>().material.color = Color.white;
        //     //   GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        //     isSelected = false;
        // }
        Color currentColor = GetComponent<Renderer>().material.color;
        if (isSelected)
        {
            currentColor.a = Mathf.Clamp01(0.3960784f);
            isSelected = false;
        }
        else
        {
            currentColor.a = Mathf.Clamp01(0f);
            isSelected = true;
        }
        GetComponent<Renderer>().material.color = currentColor;
        // Modify the alpha channel and set it back to the material




    }
    public void UnselectModel(int index)
    {
        ItemsManager.items[index].transform.GetChild(1).gameObject.GetComponent<SelectableModel>().SwitchModelSelected();
    }

}
