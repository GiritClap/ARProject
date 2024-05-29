using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameInfo2 : MonoBehaviour
{
    public TextMeshProUGUI maxScore;
    void Start()
    {
        maxScore.text = "MaxScore : " + GameManager.Instance.maxScore2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        maxScore.text = "MaxScore : " + GameManager.Instance.maxScore2.ToString();
    }

}