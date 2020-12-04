using UnityEngine;

public class PlayerPrefsInitialize : MonoBehaviour
{
    private int usedFullScreenMode;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FullScreenMode"))
        {
            PlayerPrefs.SetInt("FullScreenMode", 1);
            PlayerPrefs.Save();
        }
        usedFullScreenMode = PlayerPrefs.GetInt("FullScreenMode");
        SetFullScreenMode(usedFullScreenMode);
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
}