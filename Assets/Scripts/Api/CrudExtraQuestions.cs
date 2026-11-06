using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudExtraQuestions
    {
        // public static List<Round> GetRounds()
        // {
        //     return SingletonTeacher.instance.RoundsT;
        // }

        // public static void Create(Round round)
        // {
        //     round.Id = GetRounds().Count + 1;
        //     SingletonTeacher.instance.RoundsT.Add(round);
        // }
        // public static Round GetById(int id)
        // {
        //     return GetRounds().Find(ob => ob.Id == id);
        //     //    Singleton.instance.generalGamesT.Add(generalGame);
        // }
        public static List<Question> GetDemonsQuestions()
        {
            return SingletonTeacher.GetDemonsQuestions();
        }
        public static List<Question> GetAngelQuestions()
        {
            return SingletonTeacher.GetAngelQuestions();
        }
    }
}