using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalibrationScript : MonoBehaviour
{
    // Start is called before the first frame update
    private enum State { Start = 1, Height, Arms, Final }
    State currentState = State.Start;
    public TextMeshProUGUI display;
    public GameObject button, eyeCamera, rightHand, floor;
    public GameObject DistanceTextPrefab;
    private LineRenderer m_lineHandleRenderer;

    public Text heightText, widthText;
    private string height = "PLEASE STAND STRAIGHT AND PRESS BUTTON BELOW!";
    private string arms = "PLEASE EXTEND ARMS FORWARD AND PRESS BUTTON BELOW!";
    private string success = "CALIBRATION SUCCESS!";
    private string hellYeah = "HELL YEAH!";
    private string getHeight = "GET HEIGHT!";
    private string getWidth = "GET LENGTH!";
    private string buttonSuccess = "DONE!";

    GameObject textMeshObj;

    void Start()
    {

    }

    public void Awake()
    {
        m_lineHandleRenderer = gameObject.GetComponent<LineRenderer>();
        m_lineHandleRenderer.startWidth = 0.01f;
        m_lineHandleRenderer.endWidth = 0.01f;
        m_lineHandleRenderer.positionCount = 0;
        m_lineHandleRenderer.sortingOrder = 1;
        m_lineHandleRenderer.material = new Material(Shader.Find("Sprites/Default"));
        m_lineHandleRenderer.material.color = new Color(1, 0.8f, 0.0f, 0.7f);

        textMeshObj = Instantiate(DistanceTextPrefab, Vector3.one, Quaternion.Euler(0, 0, 180));
        textMeshObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        int test = 1;
        TextMesh textMesh = textMeshObj.GetComponentInChildren<TextMesh>();

        switch (currentState)
        {
            case State.Height:

                m_lineHandleRenderer.positionCount = 2;

                Vector3 newBottom = eyeCamera.transform.position;
                newBottom.y = floor.transform.position.y;

                m_lineHandleRenderer.SetPosition(0, newBottom + new Vector3(0.0f, 0.1f, 1f));
                m_lineHandleRenderer.SetPosition(1, eyeCamera.transform.position + new Vector3(0.0f, 0.1f, 1f));

                Vector3 midpoint = (floor.transform.position + eyeCamera.transform.position) / 2 + new Vector3(0.0f, 0.1f, 1f);
                textMeshObj.transform.position = midpoint;

                textMeshObj.transform.LookAt(2*textMeshObj.transform.position - eyeCamera.transform.position);

                textMesh.text = Vector3.Distance(floor.transform.position, eyeCamera.transform.position).ToString(".0#") + " m";
                textMesh.fontSize = 30;

                textMeshObj.SetActive(true);

                break;

            case State.Arms:

                m_lineHandleRenderer.positionCount = 2;

                m_lineHandleRenderer.SetPosition(0, eyeCamera.transform.position + new Vector3(0.15f, -0.15f, -0.06f));
                m_lineHandleRenderer.SetPosition(1, rightHand.transform.position);

                Vector3 midpoint2 = (eyeCamera.transform.position + new Vector3(0.15f, -0.15f, -0.01f) + rightHand.transform.position) / 2;
                textMeshObj.transform.position = midpoint2;

                textMeshObj.transform.LookAt(2 * textMeshObj.transform.position - eyeCamera.transform.position);

                textMesh.text = Vector3.Distance(eyeCamera.transform.position, rightHand.transform.position).ToString(".0#") + " m";
                textMesh.fontSize = 30;


                textMeshObj.SetActive(true);

                break;

            default:

                m_lineHandleRenderer.positionCount = 0;

                textMeshObj.SetActive(false);

                break;

        }
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
                float height1 = Vector3.Distance(floor.transform.position, eyeCamera.transform.position);
                Globals.height = height1;
                heightText.text = "Height: " + height1.ToString(".0##") + "m";
                currentState = State.Arms;
                

                break;
            case State.Arms:
                display.text = success;
                //initia
                button.GetComponentInChildren<Text>().text = buttonSuccess;
                float distance = Vector3.Distance(eyeCamera.transform.position ,rightHand.transform.position);
                Globals.armLength = distance;
                widthText.text = "Length: " + distance.ToString(".0##") + "m";
                currentState = State.Final;
                


                break;
            case State.Final:
                SceneManager.LoadScene("Menu");
                break;
            default:
                display.text = "OK.. TATA.";
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
