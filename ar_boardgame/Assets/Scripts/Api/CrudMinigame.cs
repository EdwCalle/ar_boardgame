using System.Collections.Generic;
using Models;

public class CrudMinigame
{

    public static List<Minigame> GetMinigames()
    {
        return Singleton.instance.minigamesT;
    }

    public static List<Minigame> GetMinigamesbyLevel(int level)
    {
        return Singleton.instance.minigamesT.FindAll(ob => ob.Level == level);
    }
}
