using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TutorialVideoController : MonoBehaviour
{

    public VideoClip jab;
    public VideoClip hook;
    public VideoClip upperCut;

    private GameObject TutorialVideoObj;
    private VideoPlayer tutorialVidPlayer;
    private string tutorialVidPlayerTag = "TutorialVid";

    void Start()
    {

        tutorialVidPlayer = GameObject.FindGameObjectWithTag(tutorialVidPlayerTag).GetComponent<VideoPlayer>();

        switch (Globals.LearnMenuInformation)
        {
            case "Jab":
                tutorialVidPlayer.clip = jab;
                tutorialVidPlayer.Play();
                    break;
            case "Hook":
                tutorialVidPlayer.clip = hook;
                tutorialVidPlayer.Play();
                    break;
            case "UpperCut":
                tutorialVidPlayer.clip = upperCut;
                tutorialVidPlayer.Play();
                    break;
            default:
                break;
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Globals.MainMenu);
    }


    public void LearnMenu()
    {
        SceneManager.LoadScene(Globals.LearnMenu);
    }
}
