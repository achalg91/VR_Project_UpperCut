using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchController : MonoBehaviour
{

    private enum HandState { None = 1, Left, Right };
    public enum PunchState { Jab = 1, Hook, UpperCut, HookJab, JabUpper, UpperHook };

    [SerializeField]
    public GameObject jabPrefab;
    [SerializeField]
    public GameObject hookPrefab;

    [SerializeField]
    public GameObject hookRightPrefab;

    [SerializeField]
    public GameObject uppercutPrefab;


    [SerializeField]
    public GameObject uppercutRightPrefab;


    private int rounds;
    private int completed;
    private GameObject punchTarget;
    private bool leftActive;
    private bool rightActive;

    public GameObject eyeCamera;
    public PunchState punchState;
    private PunchState subPunchState;

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

            updatePunchPos(HandState.Left);

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

            updatePunchPos(HandState.Right);
        }
    }

    private void updatePunchPos(HandState handState)
    {
        Vector3 newPos = refPoint.position;
        switch (punchState)
        {
            case PunchState.Hook:
                switch (handState)
                {
                    case HandState.Left:
                        newPos = refPoint.position + new Vector3(-0.5f, 0, Globals.armLength * 0.67f);
                        break;
                    case HandState.Right:
                        newPos = refPoint.position + new Vector3(0.5f, 0, Globals.armLength * 0.67f);
                        break;
                    default:
                        break;
                }
                break;
            case PunchState.Jab:
                switch (handState)
                {
                    case HandState.Left:
                        newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                        break;
                    case HandState.Right:
                        newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                        break;
                    default:
                        break;
                }
                break;
            case PunchState.UpperCut:
                switch (handState)
                {
                    case HandState.Left:
                        newPos = refPoint.position + new Vector3(-0.25f, -0.5f, Globals.armLength * 0.67f) ;
                        break;
                    case HandState.Right:
                        newPos = refPoint.position + new Vector3(0.25f, -0.5f, Globals.armLength * 0.67f);
                        break;
                    default:
                        break;
                }
                break;
            case PunchState.HookJab:
                switch(subPunchState)
                {
                    case PunchState.Hook:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(-0.5f, 0, Globals.armLength * 0.67f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.5f, 0, Globals.armLength * 0.67f);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                                break;
                            default:
                                break;
                        }
                        break;
                }
                break;
            case PunchState.JabUpper:
                switch(subPunchState)
                {
                    case PunchState.Jab:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.0f, 0, Globals.armLength * 1.0f);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(-0.25f, -0.5f, Globals.armLength * 0.67f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.25f, -0.5f, Globals.armLength * 0.67f);
                                break;
                            default:
                                break;
                        }
                        break;
                }
                break;
            case PunchState.UpperHook:
                switch(subPunchState)
                {
                    case PunchState.UpperCut:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(-0.25f, -0.5f, Globals.armLength * 0.67f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.25f, -0.5f, Globals.armLength * 0.67f);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (handState)
                        {
                            case HandState.Left:
                                newPos = refPoint.position + new Vector3(-0.5f, 0, Globals.armLength * 0.67f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0.5f, 0, Globals.armLength * 0.67f);
                                break;
                            default:
                                break;
                        }
                        break;
                }
                break;
            default:
                break;
        }

        updateHandPos(newPos);
    }

    private void updateHandPos(Vector3 newPosition)
    {
        if (punchTarget && Mathf.Abs(Vector3.Distance(punchTarget.transform.position, newPosition)) > 0.01f)
        {
            punchTarget.transform.position = Vector3.Lerp(punchTarget.transform.position, newPosition, Time.deltaTime * 5.0f);
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

        punchTarget = initPunchObjLeft();
        
        punchTarget.GetComponent<PunchSequence>().Begin(true);
        leftActive = true;
    }

    private void InitRight()
    {
        Debug.Log("UPPERCUT: CREATE RIGHT OBJ");

        punchTarget = initPunchObjRight();
        
        punchTarget.GetComponent<PunchSequence>().Begin(false);
        rightActive = true;
    }

    private GameObject initPunchObjLeft()
    {
        Vector3 startPos = eyeCamera.transform.position + 100.0f * Vector3.forward;
        GameObject ret;

        switch(punchState)
        {
            case PunchState.Hook:
                ret = Instantiate(hookPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.Jab:
                ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.UpperCut:
                ret = Instantiate(uppercutPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.HookJab:
                switch(subPunchState)
                {
                    case PunchState.Hook:
                        ret = Instantiate(hookPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;
            case PunchState.JabUpper:
                switch (subPunchState)
                {
                    case PunchState.Jab:
                        ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(uppercutPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;
            case PunchState.UpperHook:
                switch (subPunchState)
                {
                    case PunchState.UpperCut:
                        ret = Instantiate(uppercutPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(hookPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;

            default:
                ret = Instantiate(hookPrefab, startPos, Quaternion.identity);
                break;
        }

        return ret;
    }

    private GameObject initPunchObjRight()
    {
        Vector3 startPos = eyeCamera.transform.position + 100.0f * Vector3.forward;
        GameObject ret;

        switch (punchState)
        {
            case PunchState.Hook:
                ret = Instantiate(hookRightPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.Jab:
                ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.UpperCut:
                ret = Instantiate(uppercutRightPrefab, startPos, Quaternion.identity);
                break;
            case PunchState.HookJab:
                switch (subPunchState)
                {
                    case PunchState.Hook:
                        ret = Instantiate(hookRightPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;
            case PunchState.JabUpper:
                switch (subPunchState)
                {
                    case PunchState.Jab:
                        ret = Instantiate(jabPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(uppercutRightPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;
            case PunchState.UpperHook:
                switch (subPunchState)
                {
                    case PunchState.UpperCut:
                        ret = Instantiate(uppercutRightPrefab, startPos, Quaternion.identity);
                        break;
                    default:
                        ret = Instantiate(hookRightPrefab, startPos, Quaternion.identity);
                        break;

                }
                break;
            default:
                ret = Instantiate(hookRightPrefab, startPos, Quaternion.identity);
                break;
        }

        return ret;
    }

    private void CompleteRound()
    {
        completed++;
        Debug.Log("UPPERCUT: ROUND " + completed + " COMPLETE");
        if (rounds > completed)
        {
            updateSubPunchState();
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

    private void updateSubPunchState()
    {
        switch(punchState)
        {
            case PunchState.HookJab:
                if(completed % 2 == 0)
                    subPunchState = PunchState.Hook;
                else
                    subPunchState = PunchState.Jab;
                break;
            case PunchState.JabUpper:
                if (completed % 2 == 0)
                    subPunchState = PunchState.Jab;
                else
                    subPunchState = PunchState.UpperCut;
                break;
            case PunchState.UpperHook:
                if (completed % 2 == 0)
                    subPunchState = PunchState.UpperCut;
                else
                    subPunchState = PunchState.Hook;
                break;
            default:
                subPunchState = punchState;
                break;
        }
    }

}
