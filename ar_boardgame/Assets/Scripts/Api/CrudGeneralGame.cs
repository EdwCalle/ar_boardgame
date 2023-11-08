using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudGeneralGame
    {

        public static List<GeneralGame> GetGeneralGames()
        {
            return Singleton.instance.generalGamesT;
        }
        public static void Create(GeneralGame generalGame)
        {
            generalGame.Id = GetGeneralGames().Count;
            Singleton.instance.generalGamesT.Add(generalGame);
        }
        public static void Edit(GeneralGame generalGame)
        {
            GetGeneralGames()[generalGame.Id] = generalGame;

        }
        public static GeneralGame GetById(int id)
        {
            return GetGeneralGames().Find(ob => ob.Id == id);
            //    Singleton.instance.generalGamesT.Add(generalGame);
        }

        public static List<Round> GetNoSelectedRounds(int id)
        {
            List<Round> noSelectedRounds = new List<Round>();
            List<Round> selectedRounds = new List<Round>();
            GeneralGame generalGame = GetById(id);
            foreach (Round item in CrudRounds.GetRounds())
            {

                if (generalGame.Rounds.Find(ob => ob.Id == item.Id) == null)
                {
                    noSelectedRounds.Add(item);
                }
            }
            return noSelectedRounds;
        }


    }
}