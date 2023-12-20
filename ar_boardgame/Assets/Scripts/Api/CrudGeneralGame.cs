using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudGeneralGame
    {

        public static List<GeneralGame> GetGeneralGames()
        {
            return SingletonTeacher.instance.GeneralGamesT;
        }
        public static void Create(GeneralGame generalGame)
        {
            generalGame.Id = GetGeneralGames().Count + 1;
            SingletonTeacher.instance.GeneralGamesT.Add(generalGame);
        }
        public static void Edit(GeneralGame generalGame)
        {
            int i = SingletonTeacher.instance.GeneralGamesT.FindIndex(ob => ob.Id == generalGame.Id);
            SingletonTeacher.instance.GeneralGamesT[i] = generalGame;
        }
        public static void Delete(int id)
        {
            // THIS MAY CAUSE ERROR DUE TO IS SAVING DE POSITION AS ID 
            SingletonTeacher.instance.GeneralGamesT.RemoveAll(ob => ob.Id == id);

        }
        public static GeneralGame GetById(int id)
        {
            return GetGeneralGames().Find(ob => ob.Id == id);
            //    SingletonTeacher.instance.generalGamesT.Add(generalGame);
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