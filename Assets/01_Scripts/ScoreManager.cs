using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI highScoreText;

    private int highScore;

    void Start()
    {
        // Cargar el HighScore guardado
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }

    public void AddPointLeft(int point = 1)
    {
        if (leftScoreText != null)
        {
            int currentScore = int.Parse(leftScoreText.text);
            currentScore += point;
            leftScoreText.text = currentScore.ToString();
            CheckHighScore(currentScore);
        }
    }

    public void AddPointRight(int point = 1)
    {
        if (rightScoreText != null)
        {
            int currentScore = int.Parse(rightScoreText.text);
            currentScore += point;
            rightScoreText.text = currentScore.ToString();
            CheckHighScore(currentScore);
        }
    }

    private void CheckHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }
}
