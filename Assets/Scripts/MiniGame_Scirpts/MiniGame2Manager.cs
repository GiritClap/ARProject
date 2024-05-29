using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MiniGame2Manager : MonoBehaviour
{

    public TextMeshProUGUI scoreUI;
    public GameObject gameOverState;
    public GameObject ifGameOverToFalse;

    private enum State { Start, Left, Right };
    private State state;
    public GameObject[] stairs;
    private Vector3 oldPos;

    PlayerMove2 player;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Start;
        oldPos = Vector3.zero;
        player = GameObject.Find("Player").GetComponent<PlayerMove2>();
        FirstStairs();
    }


    private void FirstStairs()
    {
        for (int i = 0; i < stairs.Length; i++)
        {
            switch (state)
            {
                case State.Start:
                    stairs[i].transform.position = new Vector3(1.5f, -0.5f, 0);
                    state = State.Right;
                    break;
                case State.Left:
                    stairs[i].transform.position = oldPos + new Vector3(-1.5f, 1f, 0);

                    break;
                case State.Right:
                    stairs[i].transform.position = oldPos + new Vector3(1.5f, 1f, 0);

                    break;

            }

            oldPos = stairs[i].transform.position;

            if (i != 0)
            {
                int rand = Random.Range(0, 10);
                if (rand < 4 && i < stairs.Length - 1)
                {
                    state = state == State.Left ? State.Right : State.Left;
                }
            }
        }
    }

    public void SpawnStair(int cnt)
    {
        int rand = Random.Range(0, 10);
        if (rand < 4)
        {
            state = state == State.Left ? State.Right : State.Left;
        }

        switch (state)
        {
            case State.Left:
                stairs[cnt].transform.position = oldPos + new Vector3(-1.5f, 1f, 0);
                break;
            case State.Right:
                stairs[cnt].transform.position = oldPos + new Vector3(1.5f, 1f, 0);
                break;

        }

        oldPos = stairs[cnt].transform.position;
    }

    public void GameOver()
    {
        score = player.score;
        if (score > GameManager.Instance.maxScore2)
        {
            GameManager.Instance.maxScore2 = (int)score;
        }
        gameOverState.SetActive(true);
        ifGameOverToFalse.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
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
