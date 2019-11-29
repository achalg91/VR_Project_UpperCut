using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LearnMenuSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JabTutorial()
    {
        Globals.LearnMenuInformation = "Jab";
        LoadTutorialScene();
    }

    public void HookTutorial()
    {
        Globals.LearnMenuInformation = "Hook";
        LoadTutorialScene();
    }

    public void UpperCutTutorial()
    {
        Globals.LearnMenuInformation = "UpperCut";
        LoadTutorialScene();
    }


    public void JabPractice()
    {
        Globals.LearnMenuInformation = "Jab";
        LoadPracticeScene();
    }

    public void HookPractice()
    {
        Globals.LearnMenuInformation = "Hook";
        LoadPracticeScene();
    }

    public void UpperCutPractice()
    {
        Globals.LearnMenuInformation = "UpperCut";
        LoadPracticeScene();
    }

    void LoadTutorialScene()
    {
        SceneManager.LoadScene(Globals.Video);
    }
    void LoadPracticeScene()
    {
        SceneManager.LoadScene(Globals.Practice);
    }
}
