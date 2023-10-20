
using Models;
using UnityEngine;

public class MediatorMinigame : MonoBehaviour
{
    [SerializeField] private HeaderManager headerManager;
    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private LoadingScreenManager loadingScreenManager;

    [SerializeField] private CountdownTimer timerManager;
    //[SerializeField] private CountdownTimer countdownTimer;


    // Start is called before the first frame update
    async void Start()
    {
        // setting level from here
        // level 1
        // Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.extraInfo, SelectionType.OneSelection, ExtraComponentType.normal);
        // level 2
        //Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.normal, SelectionType.OneSelection, ExtraComponentType.normal);
        // level 3
        Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.normal, SelectionType.MultipleSelection, ExtraComponentType.normal);


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
        timerManager.PrepareLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
