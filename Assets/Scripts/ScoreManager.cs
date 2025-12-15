using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int score = 0;

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
}
