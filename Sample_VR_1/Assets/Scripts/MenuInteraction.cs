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

    public void Calibrate()
    {
        SceneManager.LoadScene("Calibration_final");   
    }

    public void Practice()
    {
        SceneManager.LoadScene("Practise");
    }

    public void Videos()
    {
        SceneManager.LoadScene("Video");
    }
}
