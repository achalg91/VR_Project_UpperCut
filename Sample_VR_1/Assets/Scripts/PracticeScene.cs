using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PracticeScene : MonoBehaviour
{
    [SerializeField]
    public GameObject timerText;

    [SerializeField]
    public GameObject videoScreen;

    [SerializeField]
    public GameObject startingText;

    public GameObject circlePrefab;

    int numRounds;
    GameObject punchTarget;
    Transform placementLeft;
    Transform placementRight;


    // Start is called before the first frame update
    void Start()
    {
        numRounds = 1;
    }

    // Update is called once per frame
    void Update()
    {
        numRounds = (int) GetComponent<Slider>().value;
        InitPractice();
    }

    private void InitPractice()
    {
        for(int i = 0; i < numRounds; i++)
        {
            punchTarget = Instantiate(circlePrefab, placementLeft.position, Camera.main.transform.rotation);
            // once target is punched
            Destroy(punchTarget);

            punchTarget = Instantiate(circlePrefab, placementRight.position, Camera.main.transform.rotation);
            // once target is punched
            Destroy(punchTarget);
        }
    }
}
