using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    public TextMeshProUGUI score3Text;

    void OnEnable()
    {
        // High Score
        int highScore = ScoreManager.instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore;

        // Top 3 Scores
        score1Text.text = "1st: " + ScoreManager.instance.GetScore1();
        score2Text.text = "2nd: " + ScoreManager.instance.GetScore2();
        score3Text.text = "3rd: " + ScoreManager.instance.GetScore3();
    }
}
