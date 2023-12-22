
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    // for game funcionality
    public GeneralGame currentGeneralGame = new();
    // [HideInInspector] public Minigame currentMinigame = new();

    //for load examples from json
    [System.Serializable]
    public class Wrapper
    {
        public Model3D[] models;
        public Minigame[] minigames;
        public Dialogue[] dialogues;
        public Round[] rounds;
        public GeneralGame[] generalGames;
    }
    public TextAsset examplesJson;
    Wrapper ExampleData;


    //for Multiplayer
    [SerializeField]
    private bool isSinglePlayerMode = false;

    // for board funcionality
    public BoardPersistent boardPersistent = new();
    public bool IsSinglePlayerMode { get => isSinglePlayerMode; set => isSinglePlayerMode = value; }

    private void Awake()
    {
        CreateSingleton();
        PreLoadDataStudent();
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
        round.Dialogue = ExampleData.dialogues[0];
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

    public static Minigame GetCurrentMinigame()
    {
        return instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Minigame;
    }

    public static bool ExistRounds()
    {
        return instance.currentGeneralGame.Rounds.Count != instance.boardPersistent.CurrentRound;
    }
    public static bool IsNotFirstRound()
    {
        return instance.boardPersistent.CurrentRound != 0;
    }
    public static bool IsSinglePlayer()
    {
        return instance.IsSinglePlayerMode;
    }
    public static int GetNumberPlayers()
    {
        return instance.boardPersistent.NumberPlayers;
    }
    public static int GetSquarePlayer(int position)
    {
        return instance.boardPersistent.SquarePlayers[position];
    }
    public static bool IsMyTurn(int currentPlayerTurn)
    {
        return instance.boardPersistent.MyTurn == currentPlayerTurn;
    }
    public static bool IsAnwserCorrect(int selectedValue, int currentPlayer)
    {
        return selectedValue == instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Questions[currentPlayer].Solution;
    }
    public static void SaveSquarePlayer(int currentPlayer, int square)
    {
        instance.boardPersistent.SquarePlayers[currentPlayer] = square;
    }
    public static string GetRoundTitle()
    {
        return instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Title;
    }
    public static string GetMinigameTitle()
    {
        return instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Minigame.Title;
    }
    public static int GetNumberRound()
    {
        return instance.boardPersistent.CurrentRound;
    }
    public static Question GetQuestionRound(int currentPlayer)
    {
        return instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Questions[currentPlayer];
    }
    public static Sentence[] GetSentences()
    {
        return instance.currentGeneralGame.Rounds[instance.boardPersistent.CurrentRound].Dialogue.Sentences;
    }


}