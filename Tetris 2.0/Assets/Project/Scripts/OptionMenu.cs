using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    private void Awake()
    {
        // Set up resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        //int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                PlayerPrefs.SetInt("currentResolutionIndex", i);
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("currentResolutionIndex");
        resolutionDropdown.RefreshShownValue();

        // Gets MasterVolume
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", .3f);
    }

    public void GoBackToMenu()
    {
        levelLoader.GoToScene(0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        FindObjectOfType<AudioManager>().Play("MenuSelect");
        Screen.fullScreen = isFullscreen;
    }

}
