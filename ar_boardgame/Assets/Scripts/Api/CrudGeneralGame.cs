using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudGeneralGame
    {
        public static List<Round> GetRounds()
        {
            return Singleton.instance.currentGeneralGame.Rounds;
        }
        public static List<GeneralGame> GetGeneralGames()
        {
            List<GeneralGame> generalGames = new List<GeneralGame>();
            generalGames.Add(Singleton.instance.currentGeneralGame);
            generalGames.Add(Singleton.instance.currentGeneralGame);
            generalGames.Add(Singleton.instance.currentGeneralGame);
            return generalGames;
        }
    }
}