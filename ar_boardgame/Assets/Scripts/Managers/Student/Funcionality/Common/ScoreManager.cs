
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

        hash["score"] = JsonUtility.ToJson(new Score { stars = 3, caps = 5 });
        Debug.Log(hash["score"]);
        hash["partialScore"] = JsonUtility.ToJson(new Score { stars = 0, caps = 0 });
        player.SetCustomProperties(hash);
    }
    public static void SetPartialSecore(Player player, Score partialScore)
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

}
