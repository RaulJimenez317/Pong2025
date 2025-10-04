using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        // Mostrar la puntuaci�n m�s alta guardada
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Cambia "GameScene" por el nombre de la escena del juego
    }

    public void QuitGame() // Cierra en el editor de unity
    {
        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false; // Cierra el juego en el editor de unity
#else
        Application.Quit(); // y este cierra el juego compilado
#endif
    }
}
