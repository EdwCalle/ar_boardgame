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
    }
}