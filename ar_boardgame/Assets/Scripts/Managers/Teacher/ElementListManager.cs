using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
public class ElementListManager : MonoBehaviour
{


    public GameObject left;
    public TextMeshProUGUI title;
    public GameObject right;
    //
    [SerializeField]
    Transform allItemsContainer;
    [SerializeField]
    Transform selectedItemsContainer;
    [SerializeField]


    public Round round;
    public List<Round> selectedRounds;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTitle(string title)
    {
        this.title.text = title;
    }
    public void PrepareData(Round round, Transform allItemsContainer, Transform selectedItemsContainer, List<Round> selectedRounds)
    {

        this.allItemsContainer = allItemsContainer;
        this.selectedItemsContainer = selectedItemsContainer;
        this.round = round;
        this.selectedRounds = selectedRounds;

        transform.SetParent(allItemsContainer);
        transform.localScale = new Vector3(1, 1, 1);
        SetTitle(this.round.Title);


    }
    public void SetRight()
    {
        transform.SetParent(selectedItemsContainer);
        SwichArrow(true);
    }
    public void SetLeft()
    {
        transform.SetParent(allItemsContainer);
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
        selectedRounds.Add(round);
    }
    void DeleteSelectedItem()
    {
        //  int pos = selectedRounds.FindIndex(obj => obj.Id == round.Id);
        // Pages.GeneralGame.
        selectedRounds.Remove(round);
    }


}
