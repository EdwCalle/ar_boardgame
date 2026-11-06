namespace Models
{

    // public enum HeaderType
    // {
    //     normal, //rest of levels:2,3,5,6,7,8,9(it has exceptions)
    //     extraInfo, // this is for level 1
    //     questions // this is for level 4
    // }
    public enum SelectionType
    {
        noSelection,
        OneSelection,
        OneTimeSelection,
        MultipleSelection
    }
    public enum ExtraComponentType
    {
        normal,
        search, // level 1,2,3
        options, //level 4
        sorter, //level 5
        completeWord, //level 6
        basket, // level 7
        assamble //level 8
    }
    public enum TimerState
    {
        none,
        offset,
        detectPlane,
        configPlane,
        interaction,
        resolve,
        feedback
    }

    public enum EditMode
    {
        create,
        edit,
        delete
    }
    public enum SquareType
    {
        normal,
        demon,
        angel,
        random
    }







}