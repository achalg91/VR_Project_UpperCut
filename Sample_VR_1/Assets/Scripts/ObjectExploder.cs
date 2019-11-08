using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExploder : MonoBehaviour
{
    private IEnumerator coroutine;

    [SerializeField]
    public GameObject mainHitObject;

    [SerializeField]
    public GameObject particlePrefab;

    [SerializeField]
    public GameObject referencePoint;

    public GameObject cameraAnchor;

    private GameObject ovrCameraRig;

    public int particleCount;

    public float particleMinSize, particleMaxSize;
    private Vector3 basePos;
    private Quaternion baseRot;

    private bool alreadyExploded;
    // Start is called before the first frame update
    void Start()
    {
        alreadyExploded = false;

        Vector3 vector = cameraAnchor.transform.position + cameraAnchor.transform.forward + cameraAnchor.transform.up;
        mainHitObject.gameObject.transform.position = vector;

        basePos = referencePoint.transform.position;
        baseRot = referencePoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!alreadyExploded)
        //{
        //    coroutine = ExplodeHitObject(10.0f);
        //    StartCoroutine(coroutine);
        //}
    }

    //IEnumerator ExplodeHitObject(float waitTime)
    //{
    //    if (!alreadyExploded)
    //    {
    //        alreadyExploded = true;
    //        yield return new WaitForSeconds(waitTime);
    //        Exploding();
    //    }
        
    //}

    IEnumerator WaitToEnableMainObject(float waitTime)
    {
        if(!alreadyExploded)
        {
            yield return new WaitForSeconds(waitTime);
            mainHitObject.SetActive(true);

            Vector3 vector = cameraAnchor.transform.position
                            + cameraAnchor.transform.forward  * Globals.armLength
                            + cameraAnchor.transform.up  * Globals.height;


            mainHitObject.gameObject.transform.position = vector;
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            mainHitObject.SetActive(true);
            alreadyExploded = false;
            Exploding();
        }
    }
    

    void Exploding()
    {
        mainHitObject.SetActive(false);

        Explode();

        coroutine = WaitToEnableMainObject(1.0f);
        StartCoroutine(coroutine);
        
        alreadyExploded = true;
    }

    void Explode()
    {
        GameObject clone;
        for (int i = 0; i < particleCount; i++)
        {
            clone = Instantiate(particlePrefab, basePos, baseRot);
            clone.transform.localScale = Random.Range(particleMinSize, particleMaxSize)
                                                                * particlePrefab.gameObject.transform.localScale;
        }
    }
}
