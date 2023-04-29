using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Enemy Param")]
    [SerializeField] float enemyRange = 1;
    [SerializeField] float atkCooldown = 3;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletForce = 16;
    [SerializeField] Transform spawnLocation;
    //[SerializeField] Animator anim;
    private Transform rbTransform;
    private Collider2D rbCollider;

    float atkTimer;

    GameObject target;

    public float enemySpeed;
    Rigidbody2D rb;

    [Header("Gun")]
    [SerializeField] GameObject gunPivot;
    Vector2 dir;
    float angle;
    Quaternion rotation;
    float rotationZ;

    [Header("Gizmo Parameters")]
    [SerializeField] Vector2 detectorSize = Vector2.one;
    [SerializeField] float detectorDelay = 0.1f;
    [SerializeField] LayerMask detectorLayerMask;
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;
    public bool playerDetected = false;
    public Vector2 offSet = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbCollider = GetComponent<Collider2D>();
        rbTransform = GetComponent<Transform>();
        StartCoroutine(DetectionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        ManageCooldowns();
        ChargePlayer();

    }


    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectorDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + offSet, detectorSize, 0, detectorLayerMask);
        if (collider != null)
        {
            playerDetected = true;
            target = collider.gameObject;
        }
        else
        {
            playerDetected = false;
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoIdleColor;
            if (playerDetected)
            {
                Gizmos.color = gizmoDetectedColor;

            }
            Gizmos.DrawCube((Vector2)transform.position + offSet, detectorSize);
        }
    }
    public void ChargePlayer()
    {
        if (playerDetected)
        {
            //AimGun();
            detectorSize = Vector2.one * 999f;
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) > enemyRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                Attack();
                rb.velocity = Vector2.zero;
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Calcular a normal
            Vector2 collisionNormal = collision.contacts[0].normal;

            //Calcular distancia dos centros (qt maior o valor da divisão menos "abrupta" é a separação)
            float overlapDistance = rbCollider.bounds.size.x / 50f + collision.collider.bounds.size.x / 50f - collision.contacts[0].separation;
            Vector3 normal3D = new Vector3(collisionNormal.x, collisionNormal.y, 0f);

            //Empurra os 2 inimigos em direções opostas para nao stackar
            rbTransform.position += normal3D * overlapDistance;
        }

    }

    public void ManageCooldowns()
    {
        atkTimer += Time.deltaTime;
    }

    private void AimGun()
    {
        dir = target.transform.position - gunPivot.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (target.transform.position.x < gunPivot.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //gunPivot.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            //gunPivot.transform.localScale = new Vector3(1, 1, 1);
        }

        gunPivot.transform.rotation = rotation;

    }

    private void Attack()
    {
        if (atkTimer > atkCooldown)
        {
            atkTimer = 0;
            GameObject blt = Instantiate(bullet, transform.position, Quaternion.identity);
            blt.GetComponent<Rigidbody2D>().AddForce((target.transform.position - blt.transform.position).normalized * bulletForce, ForceMode2D.Impulse);
            blt.transform.parent = null;
        }
    }

}
