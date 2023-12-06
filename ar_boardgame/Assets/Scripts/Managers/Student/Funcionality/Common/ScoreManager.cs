
using System;
using ExitGames.Client.Photon;
using Models;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ScoreManager
{

    public static void BeginScore(Player player)
    {
        Hashtable hash = player.CustomProperties;

        hash["score"] = JsonUtility.ToJson(new Score { stars = 0, caps = 0 });
        Debug.Log(hash["score"]);
        hash["partialScore"] = JsonUtility.ToJson(new Score { stars = 0, caps = 0 });
        player.SetCustomProperties(hash);
    }
    public static void SetPartialScore(Player player, Score partialScore)
    {
        Hashtable hash = player.CustomProperties;
        hash["partialScore"] = JsonUtility.ToJson(partialScore);
        player.SetCustomProperties(hash);
    }
    public static Score UpdatedScore(Player player)
    {
        Hashtable hash = player.CustomProperties;
        Score score = JsonUtility.FromJson<Score>((string)hash["score"]);
        Score partialScore = JsonUtility.FromJson<Score>((string)hash["partialScore"]);
        Debug.Log("Partial Score:" + partialScore.ToString() + "Score:" + score.ToString());
        score.UpdateScore(partialScore);
        Debug.Log("Score:" + score.ToString());
        hash["score"] = JsonUtility.ToJson(score);
        player.SetCustomProperties(hash);
        return score;
    }
    public static Score addStart(Player player)
    {
        Hashtable hash = player.CustomProperties;
        Score score = JsonUtility.FromJson<Score>((string)hash["score"]);
        score.Stars++;
        hash["score"] = JsonUtility.ToJson(score);
        player.SetCustomProperties(hash);
        return score;
    }
    public static Score GetMinigameResults(Minigame minigame)
    {

        if (minigame.Level == 1)
            return GetResultMinigame1(minigame);
        if (minigame.Level == 2)
            return GetResultMinigame2(minigame);
        if (minigame.Level == 3)
            return GetResultMinigame3(minigame);
        return null;
    }

    private static Score GetResultMinigame1(Minigame minigame)
    {
        int stars = 0;
        foreach (Item item in minigame.Models)
        {
            if (item.IsSolution == item.IsSelected)
                stars++;
        }
        // Score score = new() { Stars = stars };
        // score.EstimateScore();
        return new() { Stars = stars }; ;
    }
    private static Score GetResultMinigame2(Minigame minigame)
    {
        Debug.Log("GetResultMinigame2 working");

        int stars = 0;
        foreach (Item item in minigame.Models)
        {

            if (item.IsSelected)
                stars++;
        }

        // Score score = new() { Stars = stars };
        // score.EstimateScore();
        return new() { Stars = stars }; ;
    }
    private static Score GetResultMinigame3(Minigame minigame)
    {
        int stars = 0;
        foreach (Item item in minigame.Models)
        {
            if (item.Question.IsSelected == item.Question.Solution)
                stars++;
        }
        return new Score { Stars = stars };
    }


}
