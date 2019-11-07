using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    private string leftHandTag = "LeftHand";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(leftHandTag) )
        {

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
