using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour
{
    #region Singleton
    static TutorialMovement instance;
    

    public static TutorialMovement GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion


    // Start is called before the first frame update
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    
    bool isOnPlay;
    private CharacterController cc;
    bool canmove = true;
    Vector3 movement = Vector3.zero;
    private int line = 1;
    private int targetline = 1;
    public float ReducirDuracion = 5;
    public bool reducirisOn;
    public bool incrementarIsOn;
    private DistanceScore _distanceScore;
    public int speed;
    
    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
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
    async void Update()
    {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved) {
                if (!detectSwipeAfterRelease) {
                    fingerDownPos = touch.position;
                    DetectSwipe ();
                }
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended) {
                fingerDownPos = touch.position;
                DetectSwipe ();
            }
        }
        
        if (!isOnPlay) return;
        
        Vector3 pos = gameObject.transform.position;
        if (!line.Equals(targetline))
        {
            if (targetline == 0 && pos.x < -1)
            {
                gameObject.transform.position = new Vector3(-1f, pos.y, 3);
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
            }else if (targetline == 2 && pos.x > 1)
            {
                gameObject.transform.position = new Vector3(1f, pos.y, 3);
                line = targetline;
                canmove = true;
                movement.x = 0;
            }
        }
        CheckInputs();
        cc.Move(movement * Time.deltaTime);
        Zmovimiento();
    }
    
    void DetectSwipe ()
    {
		
        if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
            //Debug.Log ("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0) {
                OnSwipeUp ();
            } else if (fingerDownPos.y - fingerUpPos.y < 0) {
                OnSwipeDown ();
            }
            fingerUpPos = fingerDownPos;

        } else if (HorizontalMoveValue () > SWIPE_THRESHOLD && HorizontalMoveValue () > VerticalMoveValue ()) {
            //Debug.Log ("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0) {
                OnSwipeRight ();
            } else if (fingerDownPos.x - fingerUpPos.x < 0) {
                OnSwipeLeft ();
            }
            fingerUpPos = fingerDownPos;

        } else {
            //Debug.Log ("No Swipe Detected!");
        }
    }
    
    float VerticalMoveValue ()
    {
        return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue ()
    {
        return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp ()
    {	
        //Do something when swiped up
    }

    void OnSwipeDown ()
    {
        //Do something when swiped down
    }

    void OnSwipeLeft ()
    {
        if (canmove && line > 0)
        {
            targetline--;
            canmove = false;
            movement.x = -5;
        }
    }
    void OnSwipeRight ()
    {
        if (canmove && line <2 )
        {
            targetline++;
            canmove = false;
            movement.x = 5;
        }
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.A) && canmove && line > 0)
        {
            targetline--;
            canmove = false;
            movement.x = -5;
        }
        if (Input.GetKeyDown(KeyCode.D) && canmove && line <2 )
        {
            targetline++;
            canmove = false;
            movement.x = 5;
        }
    }

    void Zmovimiento()
    {
        speed = 20;
        movement.z = speed;
        
        if(reducirisOn == true)
        {
            ReducirVelocidad();
        }else if (incrementarIsOn == true)
        {
            IncrementarVelocidad();
        }
    }

    public void ReducirVelocidad()
    {
        if (reducirisOn)
        {
            speed = 14;
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
        if (incrementarIsOn)
        {
            speed = 35;
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
            incrementarIsOn = false;
          
        }
        
        if (other.CompareTag("PowerUpIncrementar"))
        {
            incrementarIsOn = true;
            reducirisOn = false;
            Debug.Log("Incrementar activado");
        }
        
        if (other.CompareTag("coin"))
        {
            // Lógica para recolectar la monedas
            other.gameObject.SetActive(false);  // Desactivar la moneda
        }
        
    }
}
