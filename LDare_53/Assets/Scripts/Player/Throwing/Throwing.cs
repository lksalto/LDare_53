using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwing : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float chargeTime = 1f;
    public float throwForce = 500f;
    public Image chargeBar;
    public Color minChargeColor = Color.green;
    public Color maxChargeColor = Color.red;

    private float chargeTimer = 0f;
    private void Start()
    {
        chargeBar.fillAmount = 0f;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;
            chargeBar.fillAmount = chargeTimer / chargeTime;
            chargeBar.color = Color.Lerp(minChargeColor, maxChargeColor, chargeTimer / chargeTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 direction = GetDirectionToMouse();
            rb.AddForce(direction * (chargeBar.fillAmount) * 3 * throwForce);
            chargeTimer = 0f;
            chargeBar.color = minChargeColor;
            chargeBar.fillAmount = 0;
        }
    }
    private Vector3 GetDirectionToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldPos - transform.position;
        direction.y = 0f;
        direction.Normalize();
        return direction;
    }
}
