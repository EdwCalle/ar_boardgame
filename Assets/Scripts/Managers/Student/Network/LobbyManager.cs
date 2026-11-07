using System;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private string _profileName;
    [SerializeField] private TextMeshProUGUI SessionName;
    private int _maxPlayers = 4;
    private ConnectionState _state = ConnectionState.Disconnected;
    private NetworkManager m_NetworkManager;


    private enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
    }
    private async void Awake()
    {
        _profileName = Singleton.GetPlayerName().Replace("\u200B", "").Trim();
        m_NetworkManager = GetComponent<NetworkManager>();
        // m_NetworkManager.OnClientConnectedCallback += OnClientConnectedCallback;
        // m_NetworkManager.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
        await UnityServices.InitializeAsync();
        Login();
    }
    private async void Login()
    {
        try
        {
            AuthenticationService.Instance.SwitchProfile(_profileName);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in successfully.");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
    // private void OnDestroy()
    // {
    //     _session?.LeaveAsync();
    // }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void GoWaitingRoom()
    {
        if (_state == ConnectionState.Connected)
        {
            Debug.Log("Connected to a session. Going to waiting room.");
            SceneManager.LoadScene("selectplayer");
        }
        else
            Debug.Log("Not connected to a session. Cannot go to waiting room.");
    }
    string GetRoomName()
    {
        Debug.Log("Room name: " + SessionName.text.Replace("\u200B", "").Trim());
        return SessionName.text.Replace("\u200B", "").Trim();
    }

    public async void CreateRoom()
    {


        _state = ConnectionState.Connecting;

        try
        {
            SessionOptions options = new SessionOptions()
            {
                Name = GetRoomName(),
                MaxPlayers = _maxPlayers
            }.WithDistributedAuthorityNetwork();

            ISession session = await MultiplayerService.Instance.CreateSessionAsync(options);
            Singleton.instance.SetCurrentSession(session);

            _state = ConnectionState.Connected;
            GoWaitingRoom();
        }
        catch (Exception e)
        {
            _state = ConnectionState.Disconnected;
            Debug.LogException(e);
        }
    }
    // public async void LoadSessions()
    // {
    //     try
    //     {
    //         QuerySessionsOptions options = new QuerySessionsOptions();

    //         QuerySessionsResults _results =
    //              await MultiplayerService.Instance.QuerySessionsAsync(options);

    //         foreach (ISession session in _results.Sessions)
    //         {
    //             Debug.Log(
    //                 $"Name: {session.Name} | " +
    //                 $"Players: {session.PlayerCount}/{session.MaxPlayers} | " +
    //                 $"ID: {session.Id}"
    //             );
    //         }
    //     }
    //     catch (SessionException e)
    //     {
    //         Debug.LogError(e.Message);
    //     }
    // }


    public async void JoinRoom()
    {
        _state = ConnectionState.Connecting;

        try

        {
            JoinSessionOptions options = new JoinSessionOptions()
            {

            };

            string sessionCode = GetRoomName();
            ISession session = await MultiplayerService.Instance.JoinSessionByCodeAsync(sessionCode, options);
            Singleton.instance.SetCurrentSession(session);
            _state = ConnectionState.Connected;
            GoWaitingRoom();

        }
        catch (Exception e)
        {
            _state = ConnectionState.Disconnected;
            Debug.LogException(e);
        }
    }
}

