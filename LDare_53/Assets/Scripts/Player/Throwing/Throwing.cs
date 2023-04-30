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
    [SerializeField] AudioSource audSource;
    [SerializeField] AudioSource audSourceCharge;
    [SerializeField] List<AudioClip> chargeClips;
    [SerializeField] List<AudioClip> pointClips;
    [SerializeField] List<AudioClip> throwClips;

    [SerializeField] int maxPackg;
    public int pckgCount;

    public GameMaster gm;

    private void Start()
    {
        pckgCount = maxPackg;
        //audSource = GetComponent<AudioSource>();
        chargeBar.fillAmount = 0f;
    }
    void Update()
    {
        gm.CountTotalBoxes(pckgCount, maxPackg);
        if (Input.GetMouseButton(0))
        {
            if (pckgCount > 0)
            {
                chargeTimer += Time.deltaTime;
                chargeBar.fillAmount = chargeTimer / chargeTime;
                chargeBar.color = Color.Lerp(minChargeColor, maxChargeColor, chargeTimer / chargeTime);
                if (chargeBar.color == maxChargeColor)
                {
                    audSourceCharge.Stop();
                }
            }

        }

        if(Input.GetMouseButtonDown(0))
        {
            if (pckgCount > 0)
                PlayChargeClip();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(pckgCount > 0)
            {
                audSourceCharge.Stop();
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                Vector3 direction = GetDirectionToMouse();
                rb.AddForce(direction * (chargeBar.fillAmount) * 3 * throwForce);
                chargeTimer = 0f;
                chargeBar.color = minChargeColor;
                chargeBar.fillAmount = 0;
                PlayThrowClip();
                pckgCount--;
                
            }

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

    public void PlayChargeClip()
    {
        AudioClip clipToPlay = chargeClips[Random.Range(0, chargeClips.Count - 1)];
        audSourceCharge.PlayOneShot(clipToPlay);
    }

    public void PlayPointClip()
    {
        AudioClip clipToPlay = pointClips[Random.Range(0, pointClips.Count - 1)];
        audSource.PlayOneShot(clipToPlay);
    }

    public void PlayThrowClip()
    {
        audSource.PlayOneShot(throwClips[Random.Range(0, throwClips.Count - 1)]);

    }

    public void AddPackage()
    {
        if(pckgCount < maxPackg)
        {
            pckgCount+=1;
        }
    }

}
