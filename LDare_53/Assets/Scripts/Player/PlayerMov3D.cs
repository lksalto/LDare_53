using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMov3D : MonoBehaviour
{
    Vector2 input;
    Rigidbody playerRb;
    Vector3 moveDirection;

    [SerializeField] float moveSpeed;
    [SerializeField] bool isOnRoad;
    [SerializeField] bool isOnGrass;

    [SerializeField] Vector3 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask grassLayer;
    [SerializeField] LayerMask roadLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GroundCheck();
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(input.x, 0f, input.y).normalized;
        //playerRb.AddForce(moveDirection * moveSpeed * 100f * Time.deltaTime, ForceMode.Force);
        playerRb.velocity = moveDirection * moveSpeed;
    }


    void GroundCheck()
    {
        if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, roadLayer))
        {
            isOnGrass = false;
            isOnRoad = true;
            moveSpeed = 10;
        }
        else if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, grassLayer))
        {
            isOnRoad = false;
            isOnGrass = true;
            moveSpeed = 6;
        }

    }

}
