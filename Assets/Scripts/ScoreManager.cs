using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int score = 0;
    public int GetScore1() => PlayerPrefs.GetInt("Score1", 0);
public int GetScore2() => PlayerPrefs.GetInt("Score2", 0);
public int GetScore3() => PlayerPrefs.GetInt("Score3", 0);


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetFinalScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
    public void SaveHighScore()
{
    int highScore = PlayerPrefs.GetInt("HighScore", 0);

    if (score > highScore)
    {
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
    }
    
}
public int GetHighScore()
{
    return PlayerPrefs.GetInt("HighScore", 0);
}
public void SaveTopScores()
{
    int s1 = PlayerPrefs.GetInt("Score1", 0);
    int s2 = PlayerPrefs.GetInt("Score2", 0);
    int s3 = PlayerPrefs.GetInt("Score3", 0);

    if (score > s1)
    {
        s3 = s2;
        s2 = s1;
        s1 = score;
    }
    else if (score > s2)
    {
        s3 = s2;
        s2 = score;
    }
    else if (score > s3)
    {
        s3 = score;
    }

    PlayerPrefs.SetInt("Score1", s1);
    PlayerPrefs.SetInt("Score2", s2);
    PlayerPrefs.SetInt("Score3", s3);
    PlayerPrefs.Save();
}



}
