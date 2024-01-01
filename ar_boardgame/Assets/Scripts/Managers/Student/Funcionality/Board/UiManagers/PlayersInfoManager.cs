
using UnityEngine;
public class PlayersInfoManager : MonoBehaviour
{
    public ProfileManager[] profileManager;

    public void ChangeTurn()
    {
        int currentTurn = Singleton.GetCurrentPlayerTurn();

        profileManager[currentTurn - 1].UnselectPlayer();
        profileManager[currentTurn].SelectPlayer();
    }
    public void FirstTurn()
    {
        profileManager[0].SelectPlayer();
    }

}