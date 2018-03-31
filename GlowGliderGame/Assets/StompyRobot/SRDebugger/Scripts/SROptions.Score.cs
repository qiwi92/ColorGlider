using System.ComponentModel;

public partial class SROptions
{
    private const string ScoreCategory = "Score";

    public static int StartScoreOption;

    [Category(ScoreCategory)]
    public int StartScore
    {
        get { return StartScoreOption; }
        set { StartScoreOption = value; }
    }
}