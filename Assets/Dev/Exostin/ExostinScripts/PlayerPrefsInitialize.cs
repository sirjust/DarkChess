using UnityEngine;

public class PlayerPrefsInitialize : MonoBehaviour
{
    private int usedFullScreenMode;
    private int usedGraphicsPreset;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FullScreenMode"))
        {
            PlayerPrefs.SetInt("FullScreenMode", 1);
            PlayerPrefs.Save();
        }
        usedFullScreenMode = PlayerPrefs.GetInt("FullScreenMode");
        SetFullScreenMode(usedFullScreenMode);

        if (!PlayerPrefs.HasKey("GraphicsPreset"))
        {
            PlayerPrefs.SetInt("GraphicsPreset", 0);
            PlayerPrefs.Save();
        }
        usedGraphicsPreset = PlayerPrefs.GetInt("GraphicsPreset");
        SetGraphicsPreset(usedGraphicsPreset);
    }

    public void SetFullScreenMode(int fullScreenModeIndex)
    {
        switch (fullScreenModeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution(1280, 720, false);
                break;
        }
    }

    public void SetGraphicsPreset(int graphicsPresetIndex)
    {
        switch (graphicsPresetIndex)
        {
            case 0:
                // Best
                QualitySettings.SetQualityLevel(0);
                break;

            case 1:
                // Medium
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                // Low
                QualitySettings.SetQualityLevel(2);
                break;

            case 3:
                // Literally chess
                QualitySettings.SetQualityLevel(3);
                break;
        }
    }
}