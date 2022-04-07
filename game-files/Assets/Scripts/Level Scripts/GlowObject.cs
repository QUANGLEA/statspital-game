using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObject : MonoBehaviour
{
    private MeshRenderer objectMr;
    // Start is called before the first frame update
    void Start()
    {
        objectMr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objectMr.material.EnableKeyword("_EMISSION");
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objectMr.material.DisableKeyword("_EMISSION");
        }
    }

}
