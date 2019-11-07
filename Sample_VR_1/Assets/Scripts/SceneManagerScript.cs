using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    public GameObject timerText;

    [SerializeField]
    public GameObject videoScreen;

    [SerializeField]
    public GameObject startingText;


    private TextMeshProUGUI m_textMesh;
    private UnityEngine.Video.VideoPlayer m_videoPlayer;

    private bool m_timerRunning;
    private int m_timerCount;
    // Start is called before the first frame update
    void Start()
    {
        m_textMesh = timerText.GetComponent<TextMeshProUGUI>();
        m_timerRunning = true;
        m_timerCount = 10;

        setVideo();

    }

    void setVideo()
    {
        m_videoPlayer = videoScreen.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoScreen.SetActive(false);

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
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
                videoScreen.SetActive(true);
                m_videoPlayer.Play();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
