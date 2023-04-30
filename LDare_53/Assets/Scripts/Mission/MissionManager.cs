using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class MissionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> houses;
    [SerializeField] Vector3 targetPosition;

    [SerializeField] Camera uiCamera;

    GameObject pointer;
    private RectTransform pointerRectTransform;
    GameObject player;
    GameObject target;
    SpriteRenderer targetSpriteRenderer;
    SpriteChanger targetSpriteScript;
    List<int> chosenHouses;

    [SerializeField] bool isOffScreen;
    [SerializeField]float borderSize = 100f;

    [SerializeField] float angle;
    [SerializeField] Vector3 dir;
    [SerializeField] Vector3 cappedTargetScreenPosition;
    [SerializeField] Vector3 targetPositionScreenPoint;
    [SerializeField] Vector3 pointerPosition;
    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
        pointerRectTransform = pointer.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        houses = new List<GameObject>();
        foreach(GameObject house in GameObject.FindGameObjectsWithTag("House"))
        {
            houses.Add(house);
        }
        GenerateNextHouse();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void GenerateNextHouse()
    {
        int randomNumber = Random.Range(0, houses.Count);
        while(chosenHouses.Contains(randomNumber))
        {
            randomNumber = Random.Range(0, houses.Count);
        }
        chosenHouses.Add(randomNumber);
       
        target = houses[randomNumber];
        targetPosition = target.transform.position;
        targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        targetSpriteScript = target.GetComponent<SpriteChanger>();
    }



    // Update is called once per frame
    void Update()
    {
        LookAtTarget();

        /*
        Vector3 toPosition = targetPosition;
        //Vector3 fromPosition = Camera.main.transform.position; // mudar p player?
        Vector3 fromPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        fromPosition.y = 0f;
        dir = (toPosition - fromPosition).normalized;
        angle = UtilsClass.GetAngleFromVectorFloat(dir);
        //angle = Vector3.Angle(fromPosition, toPosition);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        isOffScreen = targetPositionScreenPoint.x < borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if(isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize)
                cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize)
                cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
                cappedTargetScreenPosition.y = Screen.height - borderSize;

            //Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            //pointerRectTransform.position = pointerWorldPosition;
            //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        //*/
    }

    void LookAtTarget()
    {
        targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        Vector3 playerPosAtScreen = Camera.main.WorldToScreenPoint(player.transform.position);
        dir = (targetPositionScreenPoint - playerPosAtScreen).normalized;
        angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        pointerRectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
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

            pointerPosition = pointerRectTransform.localPosition;
            pointerRectTransform.position = cappedTargetScreenPosition;
            //Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            //pointerRectTransform.position = pointerWorldPosition;
            //pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            targetSpriteRenderer.sprite = targetSpriteScript.houseHighlight;
            pointer.SetActive(false);
        }
    }
}


