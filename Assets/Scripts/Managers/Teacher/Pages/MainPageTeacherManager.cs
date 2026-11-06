using UnityEngine;

public class MainPageTeacherManager : MonoBehaviour
{
    //public GameObject[] pages;

    //int currentActive;
    public GameObject lastGameObject;
    public void ActivatePage(GameObject selectedGameObject)
    {
        lastGameObject.SetActive(false);
        selectedGameObject.SetActive(true);
        lastGameObject = selectedGameObject;
        //   lastGameObject.GetComponent<TabManager>().List();
    }
}
