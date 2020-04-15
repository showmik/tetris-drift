using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void Awake()
    {
        foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                sound.source.volume = PlayerPrefs.GetFloat("MasterVolume");
            }
            else
            {
                sound.source.volume = 0.2f;
            }
        }
        
    }

    public void StartGame()
    {
        levelLoader.GoToScene(2);
    }

    public void GoToOption()
    {
        levelLoader.GoToScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
