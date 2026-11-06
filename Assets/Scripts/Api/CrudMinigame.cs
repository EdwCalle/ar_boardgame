using System.Collections.Generic;
using Models;

public class CrudMinigame
{

    public static List<Minigame> GetMinigames()
    {
        return SingletonTeacher.instance.MinigamesT;
    }

    public static List<Minigame> GetMinigamesbyLevel(int level)
    {
        return SingletonTeacher.instance.MinigamesT.FindAll(ob => ob.Level == level);
    }
}
