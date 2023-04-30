using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPoints : MonoBehaviour
{
    [SerializeField] bool inRange = false;
    [SerializeField] bool canPick = false;
    [SerializeField] Transform target;
    Rigidbody rb;
    [SerializeField] float maxCount = 3;
    Throwing playerSound;
    private float undetect = 0f;

    float count;

    void Start()
    {
        playerSound = FindObjectOfType<Throwing>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        undetect += Time.deltaTime;
        if(rb.velocity.x > Vector3.zero.x)
        {
            canPick = false;
        }
        else
        {
            canPick = true;
        }
        

    }

    private void OnDestroy()
    {
        if(inRange)
        {
            playerSound.PlayPointClip();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            count += Time.deltaTime;
            if(count > maxCount)
            {
                Destroy(gameObject);
            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            inRange = true;
            //Destroy(gameObject, 3f);
        }

        if (other.gameObject.CompareTag("Player") && canPick && undetect > 1f)
        {
            playerSound.AddPackage();
            Destroy(gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            canPick = true;
            inRange = false;
            count = 0;
        }
    }
}
