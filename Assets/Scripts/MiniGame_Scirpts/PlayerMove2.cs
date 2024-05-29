using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove2 : MonoBehaviour
{
    bool isRight = true;
    bool isFirst = true;
    Vector3 oldPos;

    public int score = 0;
    int spawnCnt = 0;
    MiniGame2Manager manager;
    public TextMeshProUGUI scoreText;

    public Slider hpBar;
    public GameObject hpBarColor;
    float curLife = 5;
    float lifeFull = 5;

    public GameObject arrowLeft;
    public GameObject arrowRight;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip[] clips;

    bool firstDie = true;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("MiniGame2Manager").GetComponent<MiniGame2Manager>();
        oldPos = transform.position;
        scoreText.text = "Score : " + score.ToString();
        hpBar.value = curLife / lifeFull;
        arrowRight.SetActive(true);
        arrowLeft.SetActive(false);

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        hpBar.value = curLife / lifeFull;
        curLife -= Time.deltaTime * 1.0f;
        if (curLife < 2)
        {
            hpBarColor.GetComponent<Image>().color = Color.red;
            if (curLife <= 0)
            {
                if (firstDie)
                {
                    audioSource.PlayOneShot(clips[2]);
                    firstDie = false;
                }

                animator.SetBool("isDie", true);
                manager.GameOver();
            }
        }
        else
        {
            hpBarColor.GetComponent<Image>().color = Color.green;
        }

        if (score == 15)
        {
            lifeFull = 3;
        }

        if (score == 30)
        {
            lifeFull = 2;
        }

        if (score == 40)
        {
            lifeFull = 1;
        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {


        }
        else
        {
            if (firstDie)
            {
                audioSource.PlayOneShot(clips[2]);
                firstDie = false;
            }
            Debug.Log("¿©±ä°¡? 2");
            animator.SetBool("isDie", true);
            manager.GameOver();
        }




    }

    public void MoveToggle()
    {
        audioSource.clip = clips[1];
        audioSource.Play();
        if (isRight)
        {
            if (isFirst)
            {
                this.transform.position = oldPos + new Vector3(1.5f, 1.75f, 0);
                oldPos = transform.position;
                isFirst = false;
            }
            else
            {
                this.transform.position = oldPos + new Vector3(1.5f, 1f, 0);
                oldPos = transform.position;
            }


        }
        else
        {
            if (isFirst)
            {
                this.transform.position = oldPos + new Vector3(-1.5f, 1.75f, 0);
                oldPos = transform.position;
                isFirst = false;
            }
            else
            {
                this.transform.position = oldPos + new Vector3(-1.5f, 1f, 0);
                oldPos = transform.position;
            }
        }

        score += 1;

        if (score > 5)
        {
            RespawnStairs();
        }

        curLife = lifeFull;
    }

    public void ChangeVectorToggle()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
        if (isRight)
        {
            arrowRight.SetActive(false);
            isRight = false;
            arrowLeft.SetActive(true);
        }
        else
        {
            arrowLeft.SetActive(false);
            isRight = true;
            arrowRight.SetActive(true);
        }
    }

    void RespawnStairs()
    {
        manager.SpawnStair(spawnCnt);
        spawnCnt++;

        if (spawnCnt > manager.stairs.Length - 1)
        {
            spawnCnt = 0;
        }
    }

}
