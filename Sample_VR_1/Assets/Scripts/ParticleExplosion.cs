using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : MonoBehaviour
{
    [SerializeField]
    public GameObject originalObject;

    [SerializeField]
    public GameObject particlePrefab;

    public bool alreadyExploded;

    public int particleCount;

    public float particleMinSize, particleMaxSize;

    // Start is called before the first frame update
    void Start()
    {
        alreadyExploded = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyExploded)
        {
            Exploding();
        }
    }

    void Exploding()
    {
        Explode();
        alreadyExploded = true;
    }

    void Explode()
    {
        GameObject clone;
        for (int i = 0; i < particleCount; i++)
        {
            clone = Instantiate(particlePrefab, transform.position, transform.rotation);
            clone.transform.localScale = Random.Range(particleMinSize, particleMaxSize)
                                                                * particlePrefab.gameObject.transform.localScale;


        }
    }
}
