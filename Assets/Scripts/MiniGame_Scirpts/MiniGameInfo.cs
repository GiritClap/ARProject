using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameInfo : MonoBehaviour
{

    public TextMeshProUGUI maxScore;
    void Start()
    {
        maxScore.text = "MaxScore : " + GameManager.Instance.maxScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        maxScore.text = "MaxScore : " + GameManager.Instance.maxScore.ToString();
    }
}
