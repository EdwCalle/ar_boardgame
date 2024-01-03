
using System.Collections.Generic;
using Models;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    // for game funcionality
    // public
    Match currentGeneralGame = new();
    // [HideInInspector] public Minigame currentMinigame = new();

    //for load examples from json
    [System.Serializable]
    public class Wrapper
    {
        public Model3D[] models;
        public Minigame[] minigames;
        public Dialogue[] dialogues;
        public Round[] rounds;
        public List<Question> angelQuestions;
        public List<Question> demonQuestions;
        public Match[] generalGames;
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
        currentGeneralGame = new Match(01, "Bloque 1:Las Plantas", "Ciencias Naturales");

        SetRoundExample(0, 0);
        SetRoundExample(1, 1);
        SetRoundExample(2, 2);
        SetExtraQuestion();
        // print(JsonUtility.ToJson(currentGeneralGame.Rounds[0]));
        // print(JsonUtility.ToJson(currentGeneralGame.Rounds[1]));
        // print(JsonUtility.ToJson(currentGeneralGame.[DemonQuestions]));
    }
    public void SetExtraQuestion()
    {
        currentGeneralGame.DemonQuestions = ExampleData.demonQuestions;
        currentGeneralGame.AngelQuestions = ExampleData.angelQuestions;
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
    public static bool IsMyTurn()
    {
        return instance.boardPersistent.MyTurn == instance.boardPersistent.CurrentPlayerTurn;
    }
    public static void UpdateTurn()
    {
        instance.boardPersistent.CurrentPlayerTurn++;
    }
    public static int GetCurrentPlayerTurn()
    {
        return instance.boardPersistent.CurrentPlayerTurn;
    }
    public static void ResetPlayerTurn()
    {
        instance.boardPersistent.CurrentPlayerTurn = 0;
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
    public static Question GetDemonQuestion(int position)
    {

        //instance.boardPersistent = Random.Range(0, instance.currentGeneralGame.AngelQuestions.Count);
        return instance.currentGeneralGame.DemonQuestions[position];
    }
    public static Question GetAngelQuestion(int position)
    {
        return instance.currentGeneralGame.AngelQuestions[position];
    }
    public static int GetRadomNumberQuestion(SquareType squareType)
    {
        if (squareType == SquareType.angel)
        {
            return Random.Range(0, instance.currentGeneralGame.AngelQuestions.Count);
        }
        if (squareType == SquareType.demon)
        {
            return Random.Range(0, instance.currentGeneralGame.DemonQuestions.Count);
        }
        return 0;
    }



}