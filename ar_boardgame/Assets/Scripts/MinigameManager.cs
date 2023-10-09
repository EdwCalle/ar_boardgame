
using Models;
using TMPro;
using UnityEngine;
using UnityGLTF;

public class MinigameManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject modelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Singleton.instance.PreLoadData();

        //preload all models
        foreach (ModelObject item in Singleton.instance.currentMinigame.Models)
        {
            print(item.Name);
            modelPrefab.GetComponent<GLTFComponent>().GLTFUri = item.Url;
            Instantiate(modelPrefab, item.GetPosition(), Quaternion.identity);

        }
        print("a");

        //
    }

    // Update is called once per frame
    void Update()
    {

    }
}
