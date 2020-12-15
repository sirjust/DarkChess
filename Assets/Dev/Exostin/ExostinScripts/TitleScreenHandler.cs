using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour
{
    public Animator transition;
    public float transitionDelay;

    public Animator buttons;
    public GameObject[] buttonsList; // Makes the animation adaptable to other UI buttons if needed
    List<Animator> animatorList = new List<Animator>();

    public AudioSource buttonHoverAudio;

    void Start(){
        for (int i = 0; i < buttonsList.Length; i++) 
        {
            animatorList.Add(buttonsList[i].GetComponent<Animator>()); 
            animatorList[i].enabled = false; //turn off each animator component at the start
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

     public void LaunchAudioClip()
     {
        buttonHoverAudio = GetComponent<AudioSource>();
        buttonHoverAudio.Play(0);
     }

    public void PlayButton()
    {
        StartCoroutine(LoadLevel());
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel() 
    {
        transition.SetTrigger("Start"); // Activates the trigger called "Start" in animator

        yield return new WaitForSeconds(transitionDelay); // Don't wanna load the scene too early

        SceneManager.LoadScene(1); // You need to put the number of the main scene from build settings in here
    }

    // public void HighlightPlay(){
    //     buttons.SetTrigger("OnHover");
    // }
}