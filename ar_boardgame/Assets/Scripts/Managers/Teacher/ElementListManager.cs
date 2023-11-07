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

    public Round round;

    // public static List<Round> selectedRounds;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(allItemsContainer);
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTitle(string title)
    {
        this.title.text = title;
    }
    public void PrepareData(Round round, Transform allItemsContainer, Transform selectedItemsContainer)
    {
        this.round = round;
        //  this.selectedRounds = selectedRounds;
        SetTitle(this.round.Title);

        this.allItemsContainer = allItemsContainer;
        this.selectedItemsContainer = selectedItemsContainer;

    }
    public void GoRight()
    {
        transform.SetParent(selectedItemsContainer);
        SwichArrow(true);
        AddSelectedItem();
    }
    public void GoLeft()
    {
        transform.SetParent(allItemsContainer);
        SwichArrow(false);
        DeleteSelectedItem();
    }

    public void SwichArrow(bool value)
    {
        right.SetActive(!value);
        left.SetActive(value);
    }
    void AddSelectedItem()
    {
        Pages.GeneralGame.selectedRounds.Add(round);
    }
    void DeleteSelectedItem()
    {
        //  int pos = selectedRounds.FindIndex(obj => obj.Id == round.Id);
        Pages.GeneralGame.selectedRounds.Remove(round);
    }


}
