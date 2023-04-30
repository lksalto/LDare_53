using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    private void Awake()
    {
        //aimTransform = transform.Find("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();

    }

    //Código roubado de https://www.youtube.com/watch?v=fuGQFdhSPg4&ab_channel=CodeMonkey (2:55)
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletSpawn.right * bulletForce, ForceMode2D.Force);
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if (aimDirection.x < 0)
        {
            aimTransform.eulerAngles = new Vector3(180, 0, -angle);
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
