using TMPro;
using UnityEngine;

public class ElementListManager : MonoBehaviour
{


    public GameObject left;
    public TextMeshProUGUI title;
    public GameObject right;
    //
    [HideInInspector]
    public Transform allItemsContainer;
    [HideInInspector]
    public Transform SelectedItemsContainer;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.SetParent(allItemsContainer);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTitle(string title)
    {
        this.title.text = title;
    }
    public void GoRight()
    {
        transform.SetParent(SelectedItemsContainer);
        SwichArrow(true);
    }
    public void GoLeft()
    {
        transform.SetParent(allItemsContainer);
        SwichArrow(false);
    }

    public void SwichArrow(bool value)
    {
        right.SetActive(!value);
        left.SetActive(value);
    }


}
