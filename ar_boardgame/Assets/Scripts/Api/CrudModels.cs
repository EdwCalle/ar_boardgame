using System.Collections.Generic;
using Models;

namespace Api
{
    public class CrudModels
    {
        public static List<Model3D> GetModels3D()
        {
            return SingletonTeacher.instance.Models3DT;
        }

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
    }
}