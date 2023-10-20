using Models;
using UnityEditor.Search;
using UnityEngine;

public class MediatorMinigame : MonoBehaviour
{
    [SerializeField] private HeaderManager headerManager;
    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private LoadingScreenManager loadingScreenManager;


    //[SerializeField] private CountdownTimer countdownTimer;


    // Start is called before the first frame update
    async void Start()
    {
        // disable ar session

        // show resume
        loadingScreenManager.StartAnimation();
        /// headerManager.Configure(this); is not necessary right now
        // prepara el header, no olvidar implementar personalizadamente
        headerManager.PrepareLevel();
        // prepare items
        await itemsManager.PrepareLevel();
        //prepare extra item

        //prepare footer

        // prepare timer

        // disable resume
        loadingScreenManager.StopAnimation();
        // show ar session

        // start timer
    }

    // Update is called once per frame
    void Update()
    {

    }
}
