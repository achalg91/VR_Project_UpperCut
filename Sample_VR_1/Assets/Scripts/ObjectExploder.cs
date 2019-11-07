using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExploder : MonoBehaviour
{
    private IEnumerator coroutine;

    private GameObject mainHitObject;

    [SerializeField]
    public GameObject MainHitObjectPrefab;

    [SerializeField]
    public GameObject particlePrefab;

    [SerializeField]
    public GameObject referencePoint;

    public int particleCount;

    public float particleMinSize, particleMaxSize;
    private Vector3 basePos;
    private Quaternion baseRot;

    private bool alreadyExploded;
    // Start is called before the first frame update
    void Start()
    {
        alreadyExploded = false;

        ShowHitPoint(referencePoint.transform.position, referencePoint.transform.rotation);
        basePos = referencePoint.transform.position;
        baseRot = referencePoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyExploded)
        {
            coroutine = ExplodeHitObject(5.0f);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator ExplodeHitObject(float waitTime)
    {
        alreadyExploded = true;
        yield return new WaitForSeconds(waitTime);
        Exploding();
        
    }

    public void ShowHitPoint(Vector3 pos, Quaternion rot)
    {
        mainHitObject = Instantiate(MainHitObjectPrefab, pos, rot);
    }


    /*
     * private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitItem"))
        {
            Exploding();
        }
    }
    */

    void Exploding()
    {
        Explode();
        Destroy(mainHitObject);
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
