using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PraciceSceneController : MonoBehaviour
{

    public TextMeshProUGUI displayText;
    public GameObject display;
    public AudioSource audioSource;
    public AudioClip initiating, letsGo;
    private string letsGoText = "LET'S GO!!";
    private bool m_timerRunning;
    private bool m_timerStarted;
    private bool m_timerEnded;
    private int m_timerCount = 3;
    public GameObject punchController;

    public GameObject restartButton;


    public bool InitiatingDone { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunInitiator());
        m_timerRunning = true;
        displayText.text = "INITIATING ROUND! TAKE YOUR STANCE AND GET READY";
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerStarted&&m_timerRunning)
            StartCoroutine(RunTimer());
        else if(m_timerEnded)
        {
            //code to call practise punches or call the individual scenes
        }

        if(punchController.GetComponent<PunchController>().restart)
        {
            initRestartOptions();
        }
    }

    private void initRestartOptions()
    {
        punchController.GetComponent<PunchController>().restart = false;
        float totalScore = punchController.GetComponent<PunchController>().getTotalScore();
        if (totalScore > 0.0f) { 
            display.SetActive(true);
            displayText.text = "Total Score: " + totalScore.ToString() + " pts";
        }

        restartButton.SetActive(true);

    }

    public void restart()
    {
        displayText.text = "INITIATING ROUND! TAKE YOUR STANCE AND GET READY";
        restartButton.SetActive(false);
        display.SetActive(false);
        punchController.GetComponent<PunchController>().Reset();
    }

    private IEnumerator RunInitiator()
    {
        audioSource.PlayOneShot(initiating);
        yield return new WaitForSeconds(4.0f);
        m_timerStarted = true;
    }

    private IEnumerator RunTimer()
    {
        m_timerRunning = false;
        while (m_timerCount > -1 && !m_timerEnded)
        {
            if (m_timerCount > 0)
                displayText.text = "" + m_timerCount--;
            else
            {
                displayText.text = letsGoText;
                audioSource.PlayOneShot(letsGo);
                m_timerEnded = true;
                display.SetActive(false);
                InitiatingDone = true;
                StartCoroutine(StartPunch());
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator DisplayLetsGo()
    {
        yield return new WaitForSeconds(2.0f);
        displayText.text = letsGoText;
        audioSource.PlayOneShot(letsGo);
        m_timerEnded = true;
        m_timerRunning = false;
    }

    private IEnumerator StartPunch()
    {
        yield return new WaitForSeconds(2.0f);
        punchController.GetComponent<PunchController>().Begin(getPunchState());
    }

    private PunchController.PunchState getPunchState()
    {
        PunchController.PunchState punchState = PunchController.PunchState.Jab;
        switch (Globals.LearnMenuInformation)
        {
            case "Jab":
                punchState =  PunchController.PunchState.Jab;
                break;
            case "Hook":
                punchState = PunchController.PunchState.Hook;
                break;
            case "UpperCut":
                punchState = PunchController.PunchState.UpperCut;
                break;
            case "HookJab":
                punchState = PunchController.PunchState.HookJab;
                break;
            case "JabUpper":
                punchState = PunchController.PunchState.JabUpper;
                break;
            case "UpperHook":
                punchState = PunchController.PunchState.UpperHook;
                break;
            default:
                break;
        }
        return punchState;
    }
}
