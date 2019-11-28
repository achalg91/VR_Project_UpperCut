using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyBackScript : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }


    public void LearnMenu()
    {
        SceneManager.LoadScene(LearnMenu);
    }
}
