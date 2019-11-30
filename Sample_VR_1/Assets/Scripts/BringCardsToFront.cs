using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BringCardsToFront : MonoBehaviour
{
    public Vector3 finalPosition;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
	{
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPosition, Time.deltaTime * speed);
    }
}
