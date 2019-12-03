using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

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

    public GameObject audio1;
    public GameObject audio2;
    public GameObject audio3;
    public GameObject audio4;
    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

    [SerializeField]
    public GameObject uppercutRightPrefab;

    public GameObject handIndicator;

    private int rounds;
    private int completed;
    private GameObject punchTarget;
    private bool leftActive;
    private bool rightActive;
    private float timeStart, timeEnd, totalScore;
    public GameObject eyeCamera;
    public PunchState punchState;
    private PunchState subPunchState;

    private float speed;

    Transform refPoint;

    private Vector3 startPosition;

    private readonly int DEFAULT_ROUNDS = 10;

    public GameObject scoreFlyPrefab;
    
    private List<ScoreStack> scoreStack;

    private bool updatingScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreStack = new List<ScoreStack>();
        totalScore = 0;
        rounds = 0;
        completed = 0;
        
        //Begin();

        refPoint = eyeCamera.transform;

        speed = 5.0f;

        updatingScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        updateScoreObjs();

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
                        newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
                        break;
                    case HandState.Right:
                        newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
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
                                newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
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
                                newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
                                break;
                            case HandState.Right:
                                newPos = refPoint.position + new Vector3(0f, 0, Globals.armLength * 0.55f);
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
            punchTarget.transform.position = Vector3.Lerp(punchTarget.transform.position, newPosition, Time.deltaTime * speed);
        } 
    }

    private void updateScoreObjs()
    {
        if(!updatingScore)
        {
            updatingScore = true;

            for (int i = 0; i < scoreStack.Count; i++)
            {
                scoreStack[i].gameobject.transform.position = Vector3.Lerp(scoreStack[i].gameobject.transform.position
                    , scoreStack[i].newPos, Time.deltaTime * 0.05f);
            }

            updatingScore = false;
        }
    }

    private void removeFromScoreStack(GameObject remObj)
    {
        for (int i = 0; i < scoreStack.Count; i++)
        {
            if(remObj.Equals(scoreStack[i].gameobject))
            {
                scoreStack.RemoveAt(i);
                return;
            }
        }
    }

    private void destroyRight(GameObject pt)
    {
        
     
        if (punchState == PunchState.HookJab || punchState == PunchState.JabUpper || punchState == PunchState.UpperHook)
        {
            GameObject scoreFlyObj;
            scoreFlyObj = Instantiate(scoreFlyPrefab, pt.transform.position, Quaternion.identity);
            var newPos = pt.transform.position + 10.0f * Vector3.up;

            ScoreStack scoreStackObj = new ScoreStack();
            scoreStackObj.gameobject = scoreFlyObj;
            scoreStackObj.newPos = newPos;
            scoreStack.Add(scoreStackObj);
            stopWatch.Stop();
            timeEnd = scaleTimeEndToPoints(stopWatch.ElapsedMilliseconds);

            scoreFlyObj.GetComponentInChildren<TextMesh>().text = "+" + timeEnd + " pts";
            totalScore += timeEnd;
            scoreFlyObj.transform.position = Vector3.Lerp(scoreFlyObj.transform.position, newPos, Time.deltaTime * 0.05f);
            destroyScoreInFewSecs(scoreFlyObj);
        }
        audio4.GetComponent<AudioSource>().Play();
        PlayShoot(true);
        Destroy(pt);
    }

    private float scaleTimeEndToPoints(long elapsedMilliseconds)
    {
        //return elapsedMilliseconds;

        float slope = (100f - 0f) / (4000f - 0f);
        return (int) ((100)-slope*elapsedMilliseconds);
        //return inv * 100000;
    }

    private void destroyLeft(GameObject pt)
    {
        if (punchState == PunchState.HookJab || punchState == PunchState.JabUpper || punchState == PunchState.UpperHook)
        {
            GameObject scoreFlyObj;
            scoreFlyObj = Instantiate(scoreFlyPrefab, pt.transform.position, Quaternion.identity);
            var newPos = pt.transform.position + 10.0f * Vector3.up;

            ScoreStack scoreStackObj = new ScoreStack();
            scoreStackObj.gameobject = scoreFlyObj;
            scoreStackObj.newPos = newPos;
            scoreStack.Add(scoreStackObj);
            stopWatch.Stop();
            timeEnd = scaleTimeEndToPoints(stopWatch.ElapsedMilliseconds);
            scoreFlyObj.GetComponentInChildren<TextMesh>().text = "+" + timeEnd + " pts";

            totalScore += timeEnd;
            scoreFlyObj.transform.position = Vector3.Lerp(scoreFlyObj.transform.position, newPos, Time.deltaTime * 0.05f);
            destroyScoreInFewSecs(scoreFlyObj);
        }
        audio4.GetComponent<AudioSource>().Play();
        PlayShoot(false);
        Destroy(pt);
    }

    public void Begin(PunchState punchState)
    {
        Debug.Log("UPPERCUT: GAME BEGIN");
        if (rounds <= 0)
        {
            rounds = DEFAULT_ROUNDS;
        }
        this.punchState = punchState;
        updateSubPunchState();
        InitLeft();
    }

    private void InitLeft()
    {
        Debug.Log("UPPERCUT: CREATE LEFT OBJ");
        stopWatch.Reset();
        stopWatch.Start();
        handIndicator.GetComponentsInChildren<TextMeshPro>()[0].text = "LEFT";
        handIndicator.GetComponentsInChildren<TextMeshPro>()[0].color = Color.yellow;
        handIndicator.GetComponentsInChildren<TextMeshPro>()[1].text = totalScore.ToString();
        punchTarget = initPunchObjLeft();
        
        punchTarget.GetComponent<PunchSequence>().Begin(true);
        leftActive = true;
    }

    private void InitRight()
    {
        Debug.Log("UPPERCUT: CREATE RIGHT OBJ");
        stopWatch.Reset();
        stopWatch.Start();
        handIndicator.GetComponentsInChildren<TextMeshPro>()[0].text = "RIGHT";
        handIndicator.GetComponentsInChildren<TextMeshPro>()[0].color = Color.green;
        handIndicator.GetComponentsInChildren<TextMeshPro>()[1].text = totalScore.ToString();
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
            handIndicator.GetComponentInChildren<Text>().text = "RESTART";
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

        playAudio();
    }

    private void playAudio() 
    {
        if (audio1 && audio2 && audio3)
        {
            switch (subPunchState)
            {
                case PunchState.Hook:
                    audio1.SetActive(true);
                    audio2.SetActive(false);
                    audio3.SetActive(false);
                    break;
                case PunchState.Jab:
                    audio1.SetActive(false);
                    audio2.SetActive(true);
                    audio3.SetActive(false);
                    break;
                case PunchState.UpperCut:
                    audio1.SetActive(false);
                    audio2.SetActive(false);
                    audio3.SetActive(true);
                    break;

                default:
                    break;

            }
        }
    }

    public void setLevel1()
    {
        speed = 5.0f;
    }

    public void setLevel2()
    {
        speed = 10.0f;
    }

    public void setLevel3()
    {
        speed = 15.0f;
    }

    public void Reset()
    {
        if(completed >= rounds)
        {
            totalScore = 0;

            destroyLeft(punchTarget);

            rounds = 0;
            completed = 0;

            Begin(this.punchState);

            refPoint = eyeCamera.transform;
        }
        
    }


    public void destroyScoreInFewSecs(GameObject scoreObj)
    {
        StartCoroutine(DestroyScore(scoreObj, 5.0f, false, true));
    }

    IEnumerator DestroyScore(GameObject scoreObj, float duration, bool rightHand, bool leftHand)
    {
        yield return new WaitForSeconds(duration);

        removeFromScoreStack(scoreObj);
        Destroy(scoreObj);
    }

    /**
     * Code to enable haptic
     **/

    public void PlayShoot(bool rightHanded)
    {
        if (rightHanded) StartCoroutine(Haptics(1, 1, 0.3f, true, false));
        else StartCoroutine(Haptics(1, 1, 0.3f, false, true));
    }

    IEnumerator Haptics(float frequency, float amplitude, float duration, bool rightHand, bool leftHand)
    {
        if (rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(duration);

        if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }
}
