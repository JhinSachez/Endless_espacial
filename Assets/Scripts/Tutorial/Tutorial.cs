using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text tutorialText;
    public GameObject player;
    private Movement playerMovement;
    private int tutorialStep = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<Movement>();
        ShowTutorialMessage("Arrastra el dedo para moverte de izquierda a derecha");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowTutorialMessage(string message)
    {
        tutorialText.text = message;
        tutorialText.gameObject.SetActive(true);
    }

    void HideTutorialMessage()
    {
        tutorialText.gameObject.SetActive(false);
    }

    void checkTutorialStep()
    {
        switch (tutorialStep)
        {
            case 0:
                break;
                
        }
    }
}
