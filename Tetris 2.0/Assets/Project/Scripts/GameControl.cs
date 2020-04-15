using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public int difficulty = 1;
    public static int score = 0;
    public float fallTime = 0.9f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseMenuUI;
    public LevelLoader levelLoader;

    public static bool gameIsOver = false;
    public static bool gameIsPaused = false;  

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        
        gameIsOver = false;
        gameIsPaused = false;
        gameOverPanel.SetActive(false);
        pauseMenuUI.SetActive(false);

        score = 0;
        UpdateScore();
    }

    private void Update()
    {
        if (gameIsOver)
        { 
            gameOverPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void GoToMenu()
    {
        gameOverPanel.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelLoader.GoToScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        gameOverPanel.SetActive(false);
        Destroy(gameOverPanel.gameObject);
        SceneManager.LoadScene(2);
    }

    public void IncreasDifficulty()
    {
        if (fallTime > 0.1f)
           fallTime -= (difficulty / 100f);
    }

}
