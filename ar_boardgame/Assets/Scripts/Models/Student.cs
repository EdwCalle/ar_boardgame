using Models;

public class Student
{
    private int turn;
    private string nickName;
    private Score partialScore;
    private Score score;

    public int Turn { get => turn; set => turn = value; }
    public Score Score { get => score; set => score = value; }
    public Score PartialScore { get => partialScore; set => partialScore = value; }
    public string NickName { get => nickName; set => nickName = value; }
}
