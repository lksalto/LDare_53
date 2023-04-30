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


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(input.x, 0f, input.y).normalized;
        //playerRb.AddForce(moveDirection * moveSpeed * 100f * Time.deltaTime, ForceMode.Force);
        playerRb.velocity = moveDirection * moveSpeed;
    }
}