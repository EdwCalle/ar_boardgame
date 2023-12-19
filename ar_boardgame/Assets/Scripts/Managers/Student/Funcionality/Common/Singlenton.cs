using System.Collections.Generic;
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    // for game funcionality
    public GeneralGame currentGeneralGame = new();
    [HideInInspector] public Minigame currentMinigame = new();

    //for load examples from json
    [System.Serializable]
    public class Wrapper
    {
        public Model3D[] models;
        public Minigame[] minigames;
        public Round[] rounds;
        public GeneralGame[] generalGames;
    }
    public TextAsset examplesJson;
    Wrapper ExampleData;

    // for teacher testing
    private List<GeneralGame> generalGamesT = new();
    private List<Round> roundsT = new();
    private List<Minigame> minigamesT = new();
    //for Multiplayer
    [SerializeField]
    private bool isSinglePlayerMode = false;
    // for minimage
    // private int currentQuestion = 0;
    // for board funcionality
    [SerializeField]
    private int numberPlayers = 2;
    // [SerializeField]
    private int myTurn = 0;
    //  [SerializeField]
    private int currentRound = 0;
    //  [SerializeField]
    private int currentTurn = 0;
    //[SerializeField]
    private int[] squarePlayers = { 0, 0, 0, 0 };



    public int NumberPlayers { get => numberPlayers; set => numberPlayers = value; }
    public int MyTurn { get => myTurn; set => myTurn = value; }
    public int CurrentRound { get => currentRound; set => currentRound = value; }
    public int[] SquarePlayers { get => squarePlayers; set => squarePlayers = value; }
    public int CurrentTurn { get => currentTurn; set => currentTurn = value; }
    public bool IsSinglePlayerMode { get => isSinglePlayerMode; set => isSinglePlayerMode = value; }
    public int CurrentQuestion { get; internal set; }
    public List<GeneralGame> GeneralGamesT { get => generalGamesT; set => generalGamesT = value; }
    public List<Round> RoundsT { get => roundsT; set => roundsT = value; }
    public List<Minigame> MinigamesT { get => minigamesT; set => minigamesT = value; }

    private void Awake()
    {
        CreateSingleton();
        PreLoadDataStudent();
        // PreLoadDataTeacher();
    }

    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PreLoadDataStudent()
    {
        ExampleData = JsonUtility.FromJson<Wrapper>(examplesJson.text);
        currentGeneralGame = new GeneralGame(01, "Bloque 1:Las Plantas", "Ciencias Naturales");
        SetRoundExample(0, 0);
        SetRoundExample(1, 1);
        SetRoundExample(2, 2);
        print(JsonUtility.ToJson(currentGeneralGame.Rounds[0]));
        print(JsonUtility.ToJson(currentGeneralGame.Rounds[1]));
        print(JsonUtility.ToJson(currentGeneralGame.Rounds[2]));
    }
    public void SetRoundExample(int numberRound, int idMinigame)
    {
        Round round = ExampleData.rounds[numberRound];
        SetMinigameExample(round, idMinigame);
        currentGeneralGame.Rounds.Add(round);
    }
    public void SetMinigameExample(Round round, int idMinigame)
    {
        round.Minigame = ExampleData.minigames[idMinigame];
        round.Minigame.Models.Add(ExampleData.models[0]);
        round.Minigame.Models.Add(ExampleData.models[1]);
        round.Minigame.Models.Add(ExampleData.models[2]);
    }
    // for minigame 
    public void SetMinigame()
    {
        currentMinigame = currentGeneralGame.Rounds[CurrentRound].Minigame;
    }
    public bool ExistRounds()
    {
        return currentGeneralGame.Rounds.Count != CurrentRound;
    }
    public bool IsNotFirstRound()
    {
        return currentRound != 0;
    }

}