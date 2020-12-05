using TMPro;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Settings fields")]
    public TMP_Dropdown fullScreenDropdown;

    private int usedFullScreenMode;

    private void Awake()
    {
        usedFullScreenMode = PlayerPrefs.GetInt("FullScreenMode");

        SetFullScreenMode(usedFullScreenMode);

        fullScreenDropdown.value = usedFullScreenMode;
    }

    public void SetFullScreenMode(int fullScreenModeIndex)
    {
        switch (fullScreenModeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                PlayerPrefs.SetInt("FullScreenMode", 0);
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution(1280, 720, false);
                PlayerPrefs.SetInt("FullScreenMode", 1);
                break;
        }
    }

    public void SavePreferences()
    {
        PlayerPrefs.Save();
    }
}