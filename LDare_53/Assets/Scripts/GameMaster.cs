using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameMaster : MonoBehaviour
{

    public int score;
    public float time;
    public int boxes;
    public Throwing playerBoxes;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    private void Start()
    {
        score = 0;
        boxes = playerBoxes.pckgCount;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        timeText.text = Mathf.FloorToInt(time).ToString();
    }


    public void CountTotalBoxes(int number, int max)
    {
        scoreText.text = number.ToString() + "/" + max.ToString();
    }


}
