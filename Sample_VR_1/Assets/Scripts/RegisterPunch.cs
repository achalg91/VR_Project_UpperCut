using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPunch : MonoBehaviour
{

    private bool left;
    private bool punched;
    private bool set;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UPPERCUT: PUNCH REG STARTED");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("UPPERCUT: COLLISION W TAG " + other.gameObject.tag);
        Debug.Log("UPPERCUT: COLLISION W NAME " + other.gameObject.name);
        if (set && !punched)
        {
            if (left & other.gameObject.CompareTag("LeftHand"))
            {
                this.gameObject.SetActive(false);
                this.punched = true;
            }
            else if (!left & other.gameObject.CompareTag("RightHand"))
            {
                this.gameObject.SetActive(false);
                this.punched = true;
            }
        }
    }

    public void SetLeft(bool b)
    {
        Debug.Log("UPPERCUT: SET UP COMPLETE");
        this.left = b;
        set = true;
    }

    public bool WasPunched()
    {
        return this.punched;
    }
}
