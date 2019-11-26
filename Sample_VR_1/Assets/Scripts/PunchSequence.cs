using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSequence : MonoBehaviour
{

    [SerializeField]
    GameObject front;
    [SerializeField]
    GameObject back;

    private bool firstHit;
    private bool secondHit;
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (!firstHit)
            {
                //Debug.Log("UPPERCUT: WAITING FOR FRONT OBJECT TO BE HIT");
                firstHit = front.GetComponent<RegisterPunch>().WasPunched();
            } else {
                //Debug.Log("UPPERCUT: WAITING FOR RIGHT OBJECT TO BE HIT");
                secondHit = back.GetComponent<RegisterPunch>().WasPunched();
            }
        }
    }

    public GameObject Right()
    {
        Debug.Log("UPPERCUT: Right!");
        // invert the hit circles for right hand
        float diff = Mathf.Abs(front.transform.position.x - back.transform.position.x);
        front.transform.position = front.transform.position + front.transform.right * 2 * diff;
        return this.gameObject;
    }

    public bool IsDone()
    {
        return started && firstHit && secondHit;
    }

    public void Begin(bool left)
    {
        Debug.Log("UPPERCUT: BEGIN PUNCH SEQUENCE");
        front.GetComponent<RegisterPunch>().SetLeft(left);
        back.GetComponent<RegisterPunch>().SetLeft(left);
        this.started = true;
    }
}
