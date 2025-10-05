using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    //Para activar la animacion a la sig escena
    [SerializeField]  Animator transitionAnim;

    void Start()
    {
        // Mostrar la puntuaci�n m�s alta guardada
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }
    IEnumerator ChangeToSceneGame()
    {
        transitionAnim.SetTrigger("exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");

    }

    public void StartGame()
    {
        StartCoroutine(ChangeToSceneGame());
         // Cambia "GameScene" por el nombre de la escena del juego
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

