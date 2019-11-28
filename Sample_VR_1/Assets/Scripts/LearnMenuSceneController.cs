using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void Jab()
    {
        Globals.LearnMenuInformation = "Jab";
        LoadTutorialScene();
    }

    public void Hook()
    {
        Globals.LearnMenuInformation = "Hook";
        LoadTutorialScene();
    }

    public void UpperCut()
    {
        Globals.LearnMenuInformation = "UpperCut";
        LoadTutorialScene();
    }

    void LoadTutorialScene()
    {
     SceneManager.LoadScene(Video);
    }
}
