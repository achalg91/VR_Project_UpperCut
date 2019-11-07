using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBehavior : MonoBehaviour
{
    [SerializeField]
    public float delay = 2;


    [SerializeField]
    public Vector3 sparkVelocity;

    [SerializeField]
    public float shrinkSmooth = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Random.rotation * sparkVelocity;

        Invoke("Die", delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSmooth);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
