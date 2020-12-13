using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour
{
    public void PlayButton()
    {
        // You need to put the number of the main scene from build settings in here
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}