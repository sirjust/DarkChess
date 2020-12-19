using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour
{
    public Animator lowerLevel;
    public Animator transition;
    public float transitionDelay;

    public Animator buttons;
    public GameObject[] buttonsList; // Makes the animation adaptable to other UI buttons if needed
    List<Animator> animatorList = new List<Animator>();

    void Awake(){
        // Play music on awake
        FindObjectOfType<AudioManager>().Play("FireAmbient");
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    void Start(){
        // Finding the amount of buttons I have listed on 
        for (int i = 0; i < buttonsList.Length; i++) 
        {
            animatorList.Add(buttonsList[i].GetComponent<Animator>()); 
            animatorList[i].enabled = false; //turn off animator component for each button at the start
            // I only want the one I'm hovering over to be "on" so all the buttons don't animate
        }
    }

    public void FindButton(string buttonName)
     {
        for (int i = 0; i < buttonsList.Length; i++) 
        {
            if(buttonsList[i].name == buttonName)
            {
                animatorList[i].enabled = true;
                animatorList[i].SetTrigger("OnHover"); //set the animator parameter to play the animation
                //Remember to turn off this specific animator to avoid turning when another valve is activated. i = the number of the animator in the list. if in the inspector it says: "Element 0" then this would be the same as "animatorList[0]"
            }
        }
     }


    public void PlayButton()
    {
        StartCoroutine(LoadLevel());
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    // Play screenshaking animation AND level slide up animation
    // Play relevant audio clips
    // Time delay until animation ends (WaitForSeconds)
    // Fade out
    // Delay until fade animation is finished before loading scene (WaitForSeconds)
    // Load scene
    IEnumerator LoadLevel() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        lowerLevel.SetTrigger("Lower");
        FindObjectOfType<AudioManager>().Play("StoneScrapingEffect");
        FindObjectOfType<AudioManager>().Play("StoneMovingBass");

        yield return new WaitForSeconds(2.5f);
        transition.SetTrigger("Start"); // Activates the trigger called "Start" in animator

        yield return new WaitForSeconds(transitionDelay); // Don't wanna load the scene too early

        SceneManager.LoadScene(1); // You need to put the number of the main scene from build settings in here

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}