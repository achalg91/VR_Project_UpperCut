using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Globals.MainMenu);
    }

    public void Calibrate()
    {
        SceneManager.LoadScene(Globals.Calibration);   
    }

    public void Train()
    {
        SceneManager.LoadScene(Globals.TrainMenu);
    }

    public void Learn()
    {
        SceneManager.LoadScene(Globals.LearnMenu);
    }

    public void Combo1()
    {
        SceneManager.LoadScene(Globals.COMBO_HOOKJAB);
    }

    public void Combo2()
    {
        SceneManager.LoadScene(Globals.COMBO_JABUPPER);
    }

    public void Combo3()
    {
        SceneManager.LoadScene(Globals.COMBO_UPPERHOOK);
    }

    public void Jab()
    {
        SceneManager.LoadScene(Globals.JAB);
    }

    public void Hook()
    {
        SceneManager.LoadScene(Globals.HOOK);
    }

    public void UpperCut()
    {
        SceneManager.LoadScene(Globals.UPPERCUT);
    }

}
