using Models;

public class Student
{
    //private int turn;
    private int avatarSquare = 0;
    private string nickName;
    private Score partialScore;
    private Score score = new();

    //  public int Turn { get => turn; set => turn = value; }
    public Score Score { get => score; set => score = value; }
    public Score PartialScore { get => partialScore; set => partialScore = value; }
    public string NickName { get => nickName; set => nickName = value; }
    public int AvatarSquare { get => avatarSquare; set => avatarSquare = value; }
}
