using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudGeneralGame
    {
        public List<Round> GetRounds()
        {
            return Singleton.instance.currentGeneralGame.Rounds;
        }
    }
}