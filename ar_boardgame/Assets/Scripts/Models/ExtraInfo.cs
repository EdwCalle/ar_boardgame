public class ExtraInfo
{
    private string type;
    private string info;
    public ExtraInfo()
    {

    }

    public ExtraInfo(string type)
    {
        this.type = type;
    }
    public ExtraInfo(string type, string info = null)
    {
        this.type = type;
        this.info = info;
    }
    public string Type { get => type; set => type = value; }
    public string Info { get => info; set => info = value; }
}