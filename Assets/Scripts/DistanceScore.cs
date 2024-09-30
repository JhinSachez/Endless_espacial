using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    public GameObject startPos;
    public TextMeshProUGUI scoreText;
    public GameObject scoreTextObj;

    public float distance;

    private void Start()
    {
        scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        distance = (startPos.transform.position.z + this.transform.position.z);
        scoreText.text = distance.ToString("0") + "m";
    }
}
