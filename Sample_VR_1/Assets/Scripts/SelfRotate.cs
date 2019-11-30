using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    //A float number to tweak the movement speed of the elevators in the editor
    [SerializeField]
    float rotateTrophy = 20f;

    public float xAngle, yAngle, zAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {

        transform.Rotate(rotateTrophy * Time.deltaTime, rotateTrophy * Time.deltaTime, rotateTrophy * Time.deltaTime, Space.Self);

        /*
        Quaternion targetRotation = Quaternion.AngleAxis(50, Vector3.back);
        var step = Time.deltaTime * rotateTrophy;
        transformS.rotation = Quaternion.RotateTowards(transformS.rotation, targetRotation, step);
        */

    }
}
