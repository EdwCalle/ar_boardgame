
using Unity.VisualScripting;
using UnityEngine;

public class MainPageTeacherManager : MonoBehaviour
{
    //public GameObject[] pages;

    //int currentActive;
    public GameObject lastGameObject;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ActivatePage(GameObject selectedGameObject)
    {
        lastGameObject.SetActive(false);
        selectedGameObject.SetActive(true);
        lastGameObject = selectedGameObject;
        //   lastGameObject.GetComponent<TabManager>().List();
    }
}
