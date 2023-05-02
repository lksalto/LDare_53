using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMaster : MonoBehaviour
{

    public int score;
    public float time;
    public int boxes;
    public Throwing playerBoxes;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject UIDefault;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject failScreen;
    [SerializeField] GameObject txtTime_txt;
    [SerializeField] GameObject txtTime;
    [SerializeField] GameObject txtDel;
    
    private void Start()
    {
        score = 0;
        boxes = playerBoxes.pckgCount;
    }
    private void Update()
    {


        if(Mathf.FloorToInt(time) == 0 )
        {
            FailLevel();
        }
        else
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.FloorToInt(time).ToString();
        }

    }

    public void CountTotalBoxes(int number, int max)
    {
        scoreText.text = number.ToString() + "/" + max.ToString();
    }

    public void FinishGame()
    {
        UIDefault.SetActive(false);
        endScreen.SetActive(true);
        txtTime_txt.GetComponent<TextMeshProUGUI>().text = timeText.GetComponent<TextMeshProUGUI>().text + "s";
        txtDel.GetComponent<TextMeshProUGUI>().text = (playerBoxes.maxPackg - playerBoxes.pckgCount).ToString() + "/" + playerBoxes.maxPackg.ToString();
        Time.timeScale = 0f;
    }


    public void FailLevel()
    {
        UIDefault.SetActive(false);
        failScreen.SetActive(true);
        txtTime_txt.GetComponent<TextMeshProUGUI>().text = timeText.GetComponent<TextMeshProUGUI>().text + "s";
        txtDel.GetComponent<TextMeshProUGUI>().text =  playerBoxes.pckgCount.ToString() + "/" + playerBoxes.maxPackg.ToString();
        Time.timeScale = 0f;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
