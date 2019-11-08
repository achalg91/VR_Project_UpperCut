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
    public GameObject button, eyeCamera, rightHand;
    public Text heightText, widthText;
    private string height = "PLEASE STAND STRAIGHT AND PRESS BUTTON BELOW!";
    private string arms = "PLEASE EXTEND ARMS FORWARD AND PRESS BUTTON BELOW!";
    private string success = "CALIBRATION SUCCESS!";
    private string hellYeah = "HELL YEAH!";
    private string getHeight = "GET HEIGHT!";
    private string getWidth = "GET LENGTH!";
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
                button.GetComponentInChildren<Text>().text = getHeight;
                currentState = State.Height;
                break;
            case State.Height:
                display.text = arms;
                button.GetComponentInChildren<Text>().text = getWidth;
                float height1 = Vector3.Distance(Camera.main.transform.position, eyeCamera.transform.position);
                heightText.text = "Height: " + height1.ToString(".0##") + "m";
                currentState = State.Arms;
                break;
            case State.Arms:
                display.text = success;
                //initia
                button.GetComponentInChildren<Text>().text = buttonSuccess;
                float distance = Vector3.Distance(eyeCamera.transform.position ,rightHand.transform.position);
                widthText.text = "Length: " + distance.ToString(".0##") + "m";
                currentState = State.Final;
                break;
            case State.Final:
                display.text = "MAA CHUDAA LO!";
                //initia
                button.GetComponentInChildren<Text>().text = hellYeah;
                currentState = State.Start;
                break;
            default:
                display.text = "MAA CHUDAA LO!";
                break;
        }
    }

    public void OnHover()
    {
       
        float distance = Vector3.Distance(eyeCamera.transform.position, rightHand.transform.position);
        float height =   Vector3.Distance(new Vector3(0,0,0), eyeCamera.transform.position);
        widthText.text = "Length: " + distance.ToString(".0##") + "m";
        heightText.text = "Height: " + height.ToString(".0##") + "m";
        
                
        
    }

}
