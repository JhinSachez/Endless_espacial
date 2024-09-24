using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region Singleton
    static Movement instance;

    public static Movement GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion


    // Start is called before the first frame update
    bool isOnPlay;
    private CharacterController cc;
    bool canmove = true;
    Vector3 movement = Vector3.zero;
    private int line = 1;
    private int targetline = 1;
    private DistanceScore _distanceScore;
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        _distanceScore = gameObject.GetComponent<DistanceScore>();
        
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);

    }
    
    

    void OnGameStateChanged(Game_State _gameState)
    {
        isOnPlay = _gameState == Game_State.Play;

    }

    // Update is called once per frame
    void Update()
    {

        if (!isOnPlay) return;
        
        Vector3 pos = gameObject.transform.position;
        if (!line.Equals(targetline))
        {
            if (targetline == 0 && pos.x < -2)
            {
                gameObject.transform.position = new Vector3(-2f, pos.y, 3);
                line = targetline;
                canmove = true;
                movement.x = 0;
            } else if (targetline == 1 && (pos.x > 0 || pos.x < 0))
            {
                if (line == 0 && pos.x > 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, 3);
                    line = targetline;
                    canmove = true;
                    movement.x = 0;
                } else if (line == 2 && pos.x < 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, 3);
                    line = targetline;
                    canmove = true;
                    movement.x = 0;
                }
            }else if (targetline == 2 && pos.x > 2)
            {
                gameObject.transform.position = new Vector3(2f, pos.y, 3);
                line = targetline;
                canmove = true;
                movement.x = 0;
            }
        }
        CheckInputs();
        if (!cc.isGrounded)
        {
            movement.y = 0;
        }
        cc.Move(movement * Time.deltaTime);
        movement.z = 3;
        if (_distanceScore.distance >= 200)
        {
            movement.z = 5;
        }
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.A) && canmove && line > 0)
        {
            targetline--;
            canmove = false;
            movement.x = -1.5f;
        }
        if (Input.GetKeyDown(KeyCode.D) && canmove && line <2 )
        {
            targetline++;
            canmove = false;
            movement.x = 1.5f;
        }
    }
}
