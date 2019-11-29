using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchController : MonoBehaviour
{

    private enum HandState { None = 1, Left, Right };

    [SerializeField]
    public GameObject jabPrefab;
    [SerializeField]
    public GameObject hookPrefab;

    [SerializeField]
    public GameObject hookRightPrefab;

    [SerializeField]
    public GameObject uppercutPrefab;

    private int rounds;
    private int completed;
    private GameObject punchTarget;
    private bool leftActive;
    private bool rightActive;

    public GameObject eyeCamera;

    Transform refPoint;

    private Vector3 startPosition;

    private readonly int DEFAULT_ROUNDS = 10;

    // Start is called before the first frame update
    void Start()
    {
        rounds = 0;
        completed = 0;
        
        Begin();

        refPoint = eyeCamera.transform;
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
                destroyLeft(punchTarget);

                refPoint = eyeCamera.transform;

                InitRight();
            }

            updatePosition(HandState.Right, refPoint.position + new Vector3(-0.5f, 0, Globals.armLength * 0.67f));

        } else if (rightActive)
        {
            //Debug.Log("UPPERCUT: USER NEEDS TO HIT RIGHT");
            if (punchTarget.GetComponent<PunchSequence>().IsDone())
            {
                Debug.Log("UPPERCUT: RIGHT HIT!");
                rightActive = false;
                destroyRight(punchTarget);

                refPoint = eyeCamera.transform;

                CompleteRound();
            }

            updatePosition(HandState.Left, refPoint.position + new Vector3(0.5f, 0, Globals.armLength * 0.67f));
        }
    }

    private void updatePosition(HandState handState, Vector3 newPosition)
    {
        if (punchTarget && Mathf.Abs(Vector3.Distance(punchTarget.transform.position, newPosition)) > 0.01f)
        {
            switch (handState)
            {
                case HandState.Left:
                    punchTarget.transform.position = Vector3.Lerp(punchTarget.transform.position, newPosition, Time.deltaTime * 5.0f);
                    break;
                case HandState.Right:
                    punchTarget.transform.position = Vector3.Lerp(punchTarget.transform.position, newPosition, Time.deltaTime * 5.0f);
                    break;
                default:
                    break;
            }
        }
    }

    private void destroyRight(GameObject pt)
    {
        Destroy(pt);
    }

    private void destroyLeft(GameObject pt)
    {
        Destroy(pt);
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

        Vector3 startPos = eyeCamera.transform.position + 100.0f * Vector3.forward;

        punchTarget = Instantiate(hookPrefab, startPos, Quaternion.identity);
        
        punchTarget.GetComponent<PunchSequence>().Begin(true);
        leftActive = true;
    }

    private void InitRight()
    {
        Debug.Log("UPPERCUT: CREATE RIGHT OBJ");

        Vector3 startPos = eyeCamera.transform.position + 100.0f * Vector3.forward;

        punchTarget = Instantiate(hookRightPrefab, startPos, Quaternion.identity);
        
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
