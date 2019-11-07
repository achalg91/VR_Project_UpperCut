using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PracticeScript : MonoBehaviour
{
    [SerializeField]
    public GameObject timerText;

    [SerializeField]
    public GameObject startingText;

    private TextMeshProUGUI m_textMesh;
    
    private bool m_timerRunning;
    private int m_timerCount;
    // Start is called before the first frame update
    void Start()
    {
        m_textMesh = timerText.GetComponent<TextMeshProUGUI>();
        m_timerRunning = true;
        m_timerCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerRunning)
            StartCoroutine(runTimer());
    }

    private IEnumerator runTimer()
    {
        m_timerRunning = false;
        while (m_timerCount > -1)
        {
            if (m_timerCount > 0)
                m_textMesh.text = "" + m_timerCount--;
            else
            {
                timerText.SetActive(false);
                startingText.SetActive(false);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
