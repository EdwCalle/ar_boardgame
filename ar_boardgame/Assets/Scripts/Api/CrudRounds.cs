using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudRounds
    {
        public static List<Round> GetRounds()
        {
            return Singleton.instance.roundsT;
        }

        public static void Create(Round round)
        {
            round.Id = GetRounds().Count + 1;
            Singleton.instance.roundsT.Add(round);
        }
        public static Round GetById(int id)
        {
            return GetRounds().Find(ob => ob.Id == id);
            //    Singleton.instance.generalGamesT.Add(generalGame);
        }
    }
}