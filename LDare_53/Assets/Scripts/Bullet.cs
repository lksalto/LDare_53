using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] float bulletMaxLifetime = 5f;
    [SerializeField] GameObject expPrefab;

    private void Start()
    {
        Destroy(gameObject, bulletMaxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Se colidir com um inimigo
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameObject explosion = Instantiate(expPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.25f);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
