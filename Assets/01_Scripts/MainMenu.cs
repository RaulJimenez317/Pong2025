using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        // Mostrar la puntuación más alta guardada
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Cambia "GameScene" por el nombre de la escena del juego
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
