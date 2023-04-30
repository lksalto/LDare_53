using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPoints : MonoBehaviour
{
    [SerializeField] bool inRange;
    [SerializeField] Transform target;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void OnDestroy()
    {
        if(inRange)
        {
            Debug.Log("PONTO");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            inRange = true;
            Destroy(gameObject, 3f);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            inRange = false;
        }
    }
}
