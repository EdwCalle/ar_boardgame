using UnityEngine;
using Models;
public class SelectableModel : MonoBehaviour
{

    //private Models3DManager models3DManager;
    private Models3DManager models3DManager;
    private bool isSelected = false;
    private bool isSolution;
    public int index;
    public static int lastSelectedItem = -1;

    public bool IsSolution { get => isSolution; set => isSolution = value; }
    public Models3DManager Models3DManager { get => models3DManager; set => models3DManager = value; }

    public void SelectModel()
    {
        // if the mode time is interaction just interact
        if (Singleton.GetCurrentMinigame().TimerState == TimerState.resolve)
        {
            SelectionType selection = Singleton.GetCurrentMinigame().SelectionType;
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
            else if (selection == SelectionType.OneTimeSelection)
            {
                print(index);
                FindedItem();
            }

            if (selection == SelectionType.MultipleSelection)
                // if the selection is Multyply selection 
                SwitchModelSelected();
        }

    }

    public void SetCorrect(int index)
    {
        Singleton.GetCurrentMinigame().Models[index].IsSelected = isSelected;

        if (IsSolution == isSelected)
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = Color.red;

        Color currentColor = GetComponent<Renderer>().material.color;
        currentColor.a = Mathf.Clamp01(0.3960784f);
        GetComponent<Renderer>().material.color = currentColor;

    }
    public void FindedItem()
    {
        Singleton.GetCurrentMinigame().Models[index].IsSelected = true;
        models3DManager.FoundItem(index);
        gameObject.SetActive(false);
    }
    public void SwitchModelSelected()
    {

        Color currentColor = GetComponent<Renderer>().material.color;
        if (isSelected)
        {
            currentColor.a = Mathf.Clamp01(0f);
            isSelected = false;
        }
        else
        {

            currentColor.a = Mathf.Clamp01(0.3960784f);
            isSelected = true;
        }
        GetComponent<Renderer>().material.color = currentColor;
        // Modify the alpha channel and set it back to the material
    }
    public void UnselectModel(int index)
    {
        SwitchModelSelected();
        //  mediatorMinigame.ItemsManager.Items[index].transform.GetChild(1).gameObject.GetComponent<SelectableModel>().SwitchModelSelected();
    }

}
