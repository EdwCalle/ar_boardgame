
using System;
using Models;
using Photon.Pun;
using UnityEngine;

public class MediatorMinigame : MonoBehaviourPunCallbacks
{
    [SerializeField] private HeaderManager headerManager;
    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private LoadingScreenManager loadingScreenManager;

    [SerializeField] private CountdownTimer timerManager;
    [SerializeField] private FooterManager footerManager;
    //[SerializeField] private CountdownTimer countdownTimer;

    int playersReady = 0;
    // Start is called before the first frame update
    async void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        // setting level from here
        // level 1
        if (Singleton.instance.currentMinigame.Level == 1)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.extraInfo, SelectionType.OneSelection, ExtraComponentType.normal);
        // level 2
        if (Singleton.instance.currentMinigame.Level == 2)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.normal, SelectionType.OneSelection, ExtraComponentType.normal);
        // level 3
        if (Singleton.instance.currentMinigame.Level == 3)
            Singleton.instance.currentMinigame.ConfigureLevel(HeaderType.normal, SelectionType.MultipleSelection, ExtraComponentType.normal);


        // disable ar session

        // show resume
        loadingScreenManager.StartAnimation();
        /// headerManager.Configure(this); is not necessary right now
        // prepara el header, no olvidar implementar personalizadamente
        headerManager.PrepareLevel();
        // prepare items
        await itemsManager.PrepareLevel();
        // PrepareLevel();
        Debug.Log("why should i do");
        //playersReady++;
        photonView.RPC("PlayerLoadedScene", RpcTarget.All);
        // //prepare extra item

        // //prepare footer
        // footerManager.PrepareLevel();
        // // prepare timer

        // // disable resume
        // loadingScreenManager.StopAnimation();
        // // show ar session

        // // start timer
        // timerManager.PrepareLevel();
    }
    public async void PrepareLevel()
    {
        await itemsManager.PrepareLevel();
    }
    public void ActivateFinish(bool active)
    {
        footerManager.ActivateFinish(active);
    }
    [PunRPC]
    void PlayerLoadedScene()
    {
        playersReady++;
        CheckAllPlayersLoadedScene();
    }
    void CheckAllPlayersLoadedScene()
    {
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }
    [PunRPC]
    void StartGame()
    {
        playersReady = 0;
        //prepare extra item

        //prepare footer
        footerManager.PrepareLevel();
        // prepare timer

        // disable resume
        loadingScreenManager.StopAnimation();
        // show ar session

        // start timer
        timerManager.PrepareLevel();
    }
    public void WaitingFinishGame()
    {
        photonView.RPC("PlayerFinishedGame", RpcTarget.All);
    }
    [PunRPC]
    void PlayerFinishedGame()
    {
        playersReady++;
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            FinishGame();
            // photonView.RPC("FinishGame", RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    void FinishGame()
    {
        Singleton.instance.CurrentRound++;
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("board");
    }



    //right now methods are static, so  mediator pattern design  doesn't applies totally

    // Update is called once per frame
    void Update()
    {

    }
}
