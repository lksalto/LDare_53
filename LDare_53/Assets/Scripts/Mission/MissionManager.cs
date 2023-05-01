using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class MissionManager : MonoBehaviour
{
    List<GameObject> houses;
    Vector3 targetPosition;

    GameObject pointer;
    private RectTransform pointerRectTransform;
    GameObject player;
    public GameObject target;
    SpriteRenderer targetSpriteRenderer;
    SpriteChanger targetSpriteScript;
    List<GameObject> chosenHouses;

    bool isOffScreen;
    float borderSize = 40f;
    public int numberOfDeliveries = 3;


    float angle;
    Vector3 dir;
    Vector3 cappedTargetScreenPosition;
    Vector3 targetPositionScreenPoint;
    int randomNumber;
    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        pointerRectTransform = pointer.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        houses = new List<GameObject>();
        foreach(GameObject house in GameObject.FindGameObjectsWithTag("Yard"))
        {
            houses.Add(house);
        }
        chosenHouses = new List<GameObject>();
        GenerateNextHouse();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public void GenerateNextHouse()
    {
        Debug.Log(randomNumber + " " + houses.Count);
        if (houses.Count != 0 && numberOfDeliveries > 0)
        {
            randomNumber = Random.Range(0, houses.Count);
            while (chosenHouses.Contains(houses[randomNumber]))
            {
                randomNumber = Random.Range(0, houses.Count);
            }
        }
        if (numberOfDeliveries > 0 && houses.Count != 0)
        {
            chosenHouses.Add(houses[randomNumber]);

            target = houses[randomNumber];
            targetPosition = target.transform.position;
            targetSpriteScript = target.GetComponentInChildren<SpriteChanger>();
            targetSpriteRenderer = targetSpriteScript.GetSpriteRenderer();

            houses.Remove(houses[randomNumber]);
        }
        else
        {
            targetSpriteRenderer.sprite = targetSpriteScript.house;
            //Acabar o jogo
        }
    }

    public void ResetHouseSprite()
    {
        targetSpriteRenderer.sprite = targetSpriteScript.house;
        target = null;
    }


    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        Vector3 playerPosAtScreen = Camera.main.WorldToScreenPoint(player.transform.position);
        dir = (targetPositionScreenPoint - playerPosAtScreen).normalized;
        angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        pointerRectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen && target != null)
        {
            targetSpriteRenderer.sprite = targetSpriteScript.house;
            pointer.SetActive(true);
            cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize)
                cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize)
                cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
                cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerPosition = pointerRectTransform.localPosition;
            pointerRectTransform.position = cappedTargetScreenPosition;
        }
        else if(!isOffScreen && target != null)
        {
            targetSpriteRenderer.sprite = targetSpriteScript.houseHighlight;
            pointer.SetActive(false);
        }
    }
}


