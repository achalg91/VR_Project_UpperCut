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
        if (set && !this.punched)
        {
            if (left & other.gameObject.CompareTag("LeftHand"))
            {
                this.punched = true;

                PlayShoot(false);
                
            }
            else if (!left & other.gameObject.CompareTag("RightHand"))
            {
                this.punched = true;

                PlayShoot(true);
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
        //if (rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        //if (leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

        gameObject.GetComponentInChildren<ParticleExplosion>().alreadyExploded = false;
        
        yield return new WaitForSeconds(duration);

        //if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        //if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);

        this.gameObject.SetActive(false);
    }
}
