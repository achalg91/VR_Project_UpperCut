using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationScript : MonoBehaviour
{
    // Start is called before the first frame update
    private enum State { Start = 1, Height, Arms, Final }
    State currentState = State.Start;
    public TextMeshProUGUI display;
    public GameObject button;
    private string height = "Please stand straight and press the TRIGGER button!";
    private string arms = "Your height is calibrated succesfully! Please extend your arms forward like the image and press the TRIGGER BUTTON!";
    private string success = "Calibration succesfull! Go out there and kick some ass!!";
    private string buttonSuccess = "KICK ASS!";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int test = 1;
    }

    public void UpdateState()
    {
        switch (currentState)
        {
            case State.Start:
                //enter text for height
                display.text = height;
                button.SetActive(false);
                break;
            case State.Height:
                display.text = arms;
                break;
            case State.Arms:
                display.text = success;
                //initia
                button.GetComponent<Text>().text = buttonSuccess;
                break;
            default:
                display.text = "MAA CHUDAA LO!";
                break;
        }
        currentState++;
    }

}
