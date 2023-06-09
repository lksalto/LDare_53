using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehaviour : MonoBehaviour
{
    private NavMeshAgent dog;
    private Vector3 homePoint;
    private Animator dogAnimator;
    private BoxCollider dogCollider;

    public float dogSpeed = 8;
    bool isFacingLeft;
    SpriteRenderer dogSprite;

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;

    enum states {IDLE, PATROL, CHASE}
    states state;

    void Start(){
        dog = GetComponent<NavMeshAgent>();
        dog.speed = dogSpeed;

        dogAnimator = GetComponentInChildren<Animator>();
        dogSprite = GetComponentInChildren<SpriteRenderer>();
        homePoint = transform.position;
        state = states.IDLE;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && state == states.PATROL)
            StopAllCoroutines();
        
        dog.stoppingDistance = 2;
        state = states.CHASE;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player"){
            //Debug.Log("I see you mailman");
            dogAnimator.SetBool("isChasing", true);
            dogAnimator.SetBool("isGoingHome", false);
            dogAnimator.speed = 1f;
            dog.SetDestination(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            state = states.PATROL;
            //Debug.Log("Must've been the wind");
            dogAnimator.SetBool("isChasing", false);
            dogAnimator.SetBool("isGoingHome", true);
            dogAnimator.speed = 0.8f;
            StartCoroutine(Patrulhar());
        }
    }

    private IEnumerator Patrulhar(){
        Vector3 randomPoint = homePoint;
        dog.stoppingDistance = 0;

        StartCoroutine(AwarenessTimer());

        while(state == states.PATROL){
            if(dog.velocity == Vector3.zero){
                randomPoint = GetRandomPatrolPoint();
                //Debug.Log(randomPoint);
            }
            dog.SetDestination(randomPoint);
            yield return null;
        }
    }

    public int radius = 25;
    private Vector3 GetRandomPatrolPoint(){
        Vector3 randomPoint = Random.insideUnitSphere * radius;
        Vector3 finalPoint = Vector3.zero;
        randomPoint += homePoint;
        if(NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, radius, 1))
            finalPoint = hit.position;

        return finalPoint;
    }

    private IEnumerator AwarenessTimer(){
        yield return new WaitForSeconds(10f); // adjust as needed
        BackToIdle();
    }

    private void BackToIdle(){
        state = states.IDLE;
        StopAllCoroutines();
        dog.SetDestination(homePoint);
    }

    private void Update()
    {
        if (dog.velocity.x < 0f && !isFacingLeft)
        {
            isFacingLeft = true;
            dogSprite.flipX = true;
        }
        else if (dog.velocity.x > 0f && isFacingLeft)
        {
            isFacingLeft = false;
            dogSprite.flipX = false;
        }
    }

}