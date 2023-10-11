
using Models;
using TMPro;
using UnityEngine;
using UnityGLTF;

public class MinigameManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject modelPrefab;

    // for level 1
    public GameObject image;


    // Start is called before the first frame update
    void Start()
    {
        //Singleton.instance.PreLoadData();

        //preload all models
        foreach (ModelObject item in Singleton.instance.currentMinigame.Models)
        {

            print(item.Name);
            modelPrefab.transform.GetChild(0).gameObject.GetComponent<GLTFComponent>().GLTFUri = item.Url;
            modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().center = item.GetColliderCenter();
            modelPrefab.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().size = item.GetColliderScale();
            Instantiate(modelPrefab, item.GetPosition(), Quaternion.identity);


            //en base al minijuego los modelos 3D se les asignarán los componentes que neseciten
            // un error esperar que se destruyan todos los modelos cuando se cambia de escena
        }


        //
    }

    // Update is called once per frame
    void Update()
    {

    }
}
