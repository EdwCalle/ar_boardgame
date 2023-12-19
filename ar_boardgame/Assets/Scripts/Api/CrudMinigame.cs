using System.Collections.Generic;
using Models;

public class CrudMinigame
{

    public static List<Minigame> GetMinigames()
    {
        return Singleton.instance.MinigamesT;
    }

    public static List<Minigame> GetMinigamesbyLevel(int level)
    {
        return Singleton.instance.MinigamesT.FindAll(ob => ob.Level == level);
    }
}
