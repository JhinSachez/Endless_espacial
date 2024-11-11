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
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
        scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        distance = (startPos.transform.position.z + this.transform.position.z);
        scoreText.text = distance.ToString("0") + "m";
    }
    
    void OnGameStateChange(Game_State _gs)
    {
        if (_gs == Game_State.Game_Over)
        {
            scoreTextObj.SetActive(false);
        }
        
        if (_gs == Game_State.Pause)
        {
            scoreTextObj.SetActive(false);
        }
    }
    
    
}
