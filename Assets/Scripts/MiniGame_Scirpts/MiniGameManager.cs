using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public GameObject[] obstacles;
    public GameObject gameOverState;
    float timer = 0;
    double score = 0;

    bool isLive = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI.text = "Score : " + Math.Round(timer).ToString();
        isLive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            timer += Time.deltaTime * 1.0f;
        }
        scoreUI.text = "Score : " + Math.Round(timer).ToString();
        if (timer > 10)
        {
            obstacles[0].SetActive(true);
        }
        if (timer > 20)
        {
            obstacles[1].SetActive(true);
        }


    }

    public void GameOver()
    {
        isLive = false;
        score = Math.Round(timer);
        if ((int)score > GameManager.Instance.maxScore)
        {
            GameManager.Instance.maxScore = (int)score;
        }
        gameOverState.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
