using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int p1Score = 0;
    private int p2Score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddP1Score(int value)
    {
        p1Score += value;
    }

    public void AddP2Score(int value)
    {
        p2Score += value;
    }

    public void SubtractP1Score(int value)
    {
        p1Score -= value;
        if (p1Score < 0) p1Score = 0;
    }

    public void SubtractP2Score(int value)
    {
        p2Score -= value;
        if (p2Score < 0) p2Score = 0;
    }

    public int GetP1Score()
    {
        return p1Score;
    }

    public int GetP2Score()
    {
        return p2Score;
    }

    public void ResetScores()
    {
        p1Score = 0;
        p2Score = 0;
    }
}
