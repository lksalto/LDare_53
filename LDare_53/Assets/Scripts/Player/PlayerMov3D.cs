using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMov3D : MonoBehaviour
{
    Vector2 input;
    Rigidbody playerRb;
    Vector3 moveDirection;
    Animator animator;
    bool isFacingLeft;
    bool isFacingUp;

    public GameObject horShadow;
    public GameObject verShadow;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isOnRoad;
    [SerializeField] bool isOnGrass;
    [SerializeField] ParticleSystem particleVer;
    [SerializeField] ParticleSystem particleHor;

    [SerializeField] Vector3 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask grassLayer;
    [SerializeField] LayerMask roadLayer;

    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
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
        if (input.x != 0f)
        {
            animator.SetBool("IsHorizontal", true);
        }
        else
        {
            animator.SetBool("IsHorizontal", false);
        }
        if (input.y > 0f)
            animator.SetBool("IsFacingUp", true);
        else if (input.y < 0f)
            animator.SetBool("IsFacingUp", false);

        if (input.x == 0f && input.y == 0f)
            animator.SetBool("IsIdle", true);
        else
            animator.SetBool("IsIdle", false);

        if (input.x < 0f && !isFacingLeft)
        {
            isFacingLeft = true;
            FlipPlayer();
        }
        else if (input.x > 0f && isFacingLeft)
        {
            isFacingLeft = false;
            FlipPlayer();
        }
        if(input.y > 0f && !isFacingUp)
        {
            isFacingUp = true;
            FlipShadow();
        }
        else if(input.y < 0f && isFacingUp)
        {
            isFacingUp = false;
            FlipShadow();
        }
        if(animator.GetBool("IsHorizontal"))
        {
            verShadow.SetActive(false);
            horShadow.SetActive(true);
            particleHor.Play();
        }
        else if(!animator.GetBool("IsIdle"))
        {
            horShadow.SetActive(false);
            verShadow.SetActive(true);
            particleVer.Play();
        }

        //playerRb.AddForce(moveDirection * moveSpeed * 100f * Time.deltaTime, ForceMode.Force);
        playerRb.velocity = moveDirection * moveSpeed;
    }

    private void FlipPlayer()
    {
        // Rotate around y axis
        playerSprite.transform.Rotate(0f, 180f, 0f);
        horShadow.transform.Rotate(0f, 180f, 0f);
    }

    private void FlipShadow()
    {
        verShadow.transform.Rotate(180f, 0f, 0f);
    }

    void GroundCheck()
    {
        if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, roadLayer))
        {
            isOnGrass = false;
            isOnRoad = true;
            moveSpeed = 10;
            animator.speed = 1f;
        }
        else if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, grassLayer))
        {
            isOnRoad = false;
            isOnGrass = true;
            moveSpeed = 6;
            animator.speed = 0.5f;
        }

    }

}
