using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 3;
    [SerializeField] float dieTime = 0;


    public void TakeDamage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            //morrer
            Destroy(gameObject, dieTime);
        }
    }
}
