using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int life = 9;
    public float dieTime = 0f;
    [SerializeField] float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        transform.Translate(inputVector * speed * Time.deltaTime);

    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            //morrer
            Destroy(gameObject, dieTime);
        }
    }
}
