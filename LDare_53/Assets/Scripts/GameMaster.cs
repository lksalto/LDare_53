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
        time -= Time.deltaTime;
        timeText.text = Mathf.FloorToInt(time).ToString();
    }

    public void CountTotalBoxes(int number, int max)
    {
        scoreText.text = number.ToString() + "/" + max.ToString();
    }

    public void FinishGame()
    {
        UIDefault.SetActive(false);
        endScreen.SetActive(true);
        txtTime_txt.GetComponent<TextMeshProUGUI>().text = txtTime.GetComponent<TextMeshProUGUI>().text;
        txtDel.GetComponent<TextMeshProUGUI>().text = (playerBoxes.maxPackg - playerBoxes.pckgCount).ToString() + "/" + playerBoxes.maxPackg.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
