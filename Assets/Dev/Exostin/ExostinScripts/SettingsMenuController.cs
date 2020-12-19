using TMPro;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Settings fields")]
    public TMP_Dropdown fullScreenDropdown;

    public TMP_Dropdown graphicsPresetsDropdown;

    private int usedFullScreenMode;
    private int usedGraphicsPreset;

    private void Awake()
    {
        usedFullScreenMode = PlayerPrefs.GetInt("FullScreenMode");
        usedGraphicsPreset = PlayerPrefs.GetInt("GraphicsPreset");

        SetFullScreenMode(usedFullScreenMode);
        SetGraphicsPreset(usedGraphicsPreset);

        graphicsPresetsDropdown.value = usedGraphicsPreset;
        fullScreenDropdown.value = usedFullScreenMode;
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
        PlayerPrefs.SetInt("FullScreenMode", fullScreenModeIndex);
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
        PlayerPrefs.SetInt("GraphicsPreset", graphicsPresetIndex);
        Debug.Log("Current graphics preset: " + QualitySettings.GetQualityLevel());
    }

    public void SavePreferences()
    {
        PlayerPrefs.Save();
    }
}