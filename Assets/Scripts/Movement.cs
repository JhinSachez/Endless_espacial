using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
     bool isOnPlay;
    private CharacterController cc;
    bool canmove = true;
    Vector3 movement = Vector3.zero;
    private int line = 1;
    private int targetline = 1;
    public float ReducirDuracion = 5;
    public bool reducirisOn;
    private bool incrementarIsOn;
    private DistanceScore _distanceScore;
    public int speed;
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
        if (_distanceScore.distance >= 50 && pos.y >= 0.5f)
        {
            movement.y = 5;
            if (_distanceScore.distance >= 50 && pos.y >= 5)
            {
                movement.y = 0;
            }
        }
        cc.Move(movement * Time.deltaTime);
        Zmovimiento();
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

    void Zmovimiento()
    {
        speed = 3;
        movement.z = speed;
        if (_distanceScore.distance >= 20 && reducirisOn == false && incrementarIsOn == false)
        {
            speed = 5;
            movement.z = speed;
            if (_distanceScore.distance >= 40)
            {
                speed = 7;
                movement.z = speed;
            }
        }
        else if(reducirisOn == true)
        {
            ReducirVelocidad();
        }else if (incrementarIsOn == true)
        {
            IncrementarVelocidad();
        }
    }

    public void ReducirVelocidad()
    {
        if (reducirisOn == true)
        {
            speed = 1;
            ReducirDuracion -= Time.deltaTime;
            movement.z = speed;
            if (ReducirDuracion <= 0)
            {
                ReducirDuracion = 5;
                reducirisOn = false;
            }
        }

    }

    public void IncrementarVelocidad()
    {
        if (incrementarIsOn == true)
        {
            speed = 10;
            ReducirDuracion -= Time.deltaTime;
            movement.z = speed;

            if (ReducirDuracion <= 0)
            {
                ReducirDuracion = 5;
                incrementarIsOn = false;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpRedicir"))
        {
            reducirisOn = true;
        }
        
        if (other.CompareTag("PowerUpIncrementar"))
        {
            incrementarIsOn = true;
        }
    }
}
