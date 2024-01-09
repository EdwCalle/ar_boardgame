using System;
using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudMatchGame
    {

        public static List<Match> GetMatches()
        {
            return SingletonTeacher.instance.MatchesT;
        }
        public static void Create(Match match)
        {
            match.Id = GetMatches().Count + 1;
            SingletonTeacher.instance.MatchesT.Add(match);
        }
        public static void Edit(Match generalGame)
        {
            int i = SingletonTeacher.instance.MatchesT.FindIndex(ob => ob.Id == generalGame.Id);
            SingletonTeacher.instance.MatchesT[i] = generalGame;
        }
        public static void Delete(int id)
        {
            // THIS MAY CAUSE ERROR DUE TO IS SAVING DE POSITION AS ID 
            SingletonTeacher.instance.MatchesT.RemoveAll(ob => ob.Id == id);
        }
        public static Match GetById(int id)
        {
            return GetMatches().Find(ob => ob.Id == id);
            //    SingletonTeacher.instance.generalGamesT.Add(generalGame);
        }
        public static List<Round> GetNoSelectedRounds(int id)
        {
            List<Round> noSelectedRounds = new();
            List<Round> selectedRounds = new();
            Match generalGame = GetById(id);
            foreach (Round item in CrudRounds.GetRounds())
            {

                if (generalGame.Rounds.Find(ob => ob.Id == item.Id) == null)
                {
                    noSelectedRounds.Add(item);
                }
            }
            return noSelectedRounds;
        }
        public static List<Question> GetNoSelectedQuestions(int id, String type)
        {
            List<Question> noSelectedQuestions = new();
            Match generalGame = GetById(id);
            List<Question> AllQuestions = new();
            if (type == "angels")
                AllQuestions = CrudExtraQuestions.GetAngelQuestions();
            if (type == "demons")
                AllQuestions = CrudExtraQuestions.GetDemonsQuestions();
            foreach (Question item in AllQuestions)
            {
                if (type == "angels" && generalGame.AngelQuestions.Find(ob => ob.Id == item.Id) == null)
                {
                    noSelectedQuestions.Add(item);
                }
                if (type == "demons" && generalGame.DemonQuestions.Find(ob => ob.Id == item.Id) == null)
                {
                    noSelectedQuestions.Add(item);
                }
            }
            return noSelectedQuestions;
        }
    }
}