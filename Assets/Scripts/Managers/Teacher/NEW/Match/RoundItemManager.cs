using Models;
using TMPro;
using UnityEngine;
public class RoundItemManager : MonoBehaviour
{
    public GameObject left;
    public TextMeshProUGUI title;
    public GameObject right;
    //
    [SerializeField]

    public Round round;
    public SelectorManager selectorManager;

    public void SetTitle(string title)
    {
        this.title.text = title;
    }
    public void PrepareData(Round round, SelectorManager selectorManager, bool direction)
    {
        this.round = round;
        this.selectorManager = selectorManager;
        SetTitle(this.round.Title);
        if (direction)
            SetRight();
        else
            SetLeft();

    }
    public void SetRight()
    {
        transform.SetParent(selectorManager.selectedItemsContainer);
        SwichArrow(true);
    }
    public void SetLeft()
    {
        transform.SetParent(selectorManager.allItemsContainer);
        SwichArrow(false);
    }
    public void GoRight()
    {
        SetRight();
        AddSelectedItem();
    }
    public void GoLeft()
    {
        SetLeft();
        DeleteSelectedItem();
    }

    public void SwichArrow(bool value)
    {
        right.SetActive(!value);
        left.SetActive(value);
    }
    void AddSelectedItem()
    {
        selectorManager.SelectedRounds.Add(round);
    }
    void DeleteSelectedItem()
    {
        selectorManager.SelectedRounds.Remove(round);
    }


}
