using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchController : MonoBehaviour
{

    [SerializeField]
    public GameObject jabPrefab;
    [SerializeField]
    public GameObject hookPrefab;
    [SerializeField]
    public GameObject uppercutPrefab;

    private int rounds;
    private int completed;
    private GameObject punchTarget;
    private bool leftActive;
    private bool rightActive;

    Transform refPoint;

    private readonly int DEFAULT_ROUNDS = 2;

    // Start is called before the first frame update
    void Start()
    {
        rounds = 0;
        completed = 0;
        refPoint = Camera.main.transform;
        Begin();
    }

    // Update is called once per frame
    void Update()
    {
        if (punchTarget == null)
        {
            return;
        }
        if (leftActive)
        {
            //Debug.Log("UPPERCUT: USER NEEDS TO HIT LEFT");
            if (punchTarget.GetComponent<PunchSequence>().IsDone())
            {
                Debug.Log("UPPERCUT: LEFT HIT!");
                leftActive = false;
                Destroy(punchTarget);
                InitRight();
            }
        } else if (rightActive)
        {
            //Debug.Log("UPPERCUT: USER NEEDS TO HIT RIGHT");
            if (punchTarget.GetComponent<PunchSequence>().IsDone())
            {
                Debug.Log("UPPERCUT: RIGHT HIT!");
                rightActive = false;
                Destroy(punchTarget);
                CompleteRound();
            }
        }
    }

    private void Begin()
    {
        Debug.Log("UPPERCUT: GAME BEGIN");
        if (rounds <= 0)
        {
            rounds = DEFAULT_ROUNDS;
        }
        InitLeft();
    }

    private void InitLeft()
    {
        Debug.Log("UPPERCUT: CREATE LEFT OBJ");
        punchTarget = Instantiate(hookPrefab);
        punchTarget.transform.position = refPoint.position + refPoint.up - refPoint.right;// + (Camera.main.transform.forward * 0.1f) + (Camera.main.transform.up* 5.0f);
        punchTarget.transform.localScale *= 10;
        punchTarget.GetComponent<PunchSequence>().Begin(true);
        leftActive = true;
    }

    private void InitRight()
    {
        Debug.Log("UPPERCUT: CREATE RIGHT OBJ");
        punchTarget = Instantiate(hookPrefab.GetComponent<PunchSequence>().Right());
        punchTarget.transform.position = refPoint.position + refPoint.up + refPoint.right;// + (Camera.main.transform.forward * 0.12f) + (Camera.main.transform.up * 5.0f); // + (Camera.main.transform.right * 2.0f) 
        punchTarget.transform.localScale *= 10;
        punchTarget.GetComponent<PunchSequence>().Begin(false);
        rightActive = true;
    }

    private void CompleteRound()
    {
        completed++;
        Debug.Log("UPPERCUT: ROUND " + completed + " COMPLETE");
        if (rounds > completed)
        {
            InitLeft();
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void SetRounds(int x)
    {
        if (x >= 0)
        {
            this.rounds = x;
        }
    }


}
