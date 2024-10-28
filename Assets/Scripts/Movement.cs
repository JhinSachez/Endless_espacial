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

    // Variables de movimiento y control
    bool isOnPlay;
    private CharacterController cc;
    bool canMove = true;
    Vector3 movement = Vector3.zero;
    private int line = 1;
    private int targetLine = 1;
    public float ReducirDuracion = 5;
    public bool reducirIsOn;
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

    void Update()
    {
        if (!isOnPlay) return;

        Vector3 pos = gameObject.transform.position;

        // Movimiento lateral entre líneas
        if (!line.Equals(targetLine))
        {
            if (targetLine == 0 && pos.x < -2)
            {
                gameObject.transform.position = new Vector3(-2f, pos.y, 3);
                line = targetLine;
                canMove = true;
                movement.x = 0;
            }
            else if (targetLine == 1 && (pos.x > 0 || pos.x < 0))
            {
                if (line == 0 && pos.x > 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, 3);
                    line = targetLine;
                    canMove = true;
                    movement.x = 0;
                }
                else if (line == 2 && pos.x < 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, 3);
                    line = targetLine;
                    canMove = true;
                    movement.x = 0;
                }
            }
            else if (targetLine == 2 && pos.x > 2)
            {
                gameObject.transform.position = new Vector3(2f, pos.y, 3);
                line = targetLine;
                canMove = true;
                movement.x = 0;
            }
        }

        CheckInputs();
        cc.Move(movement * Time.deltaTime); // Mueve al personaje en el eje Z.
        Zmovimiento();
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.A) && canMove && line > 0)
        {
            targetLine--;
            canMove = false;
            movement.x = -1.5f; // Mueve a la izquierda
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove && line < 2)
        {
            targetLine++;
            canMove = false;
            movement.x = 1.5f; // Mueve a la derecha
        }
    }

    void Zmovimiento()
    {
        speed = 5;
        movement.z = speed;

        if (_distanceScore.distance >= 20 && !reducirIsOn && !incrementarIsOn)
        {
            speed = 10;
            if (_distanceScore.distance >= 40) speed = 7;
            if (_distanceScore.distance >= 60) speed = 10;
        }
        else if (reducirIsOn)
        {
            ReducirVelocidad();
        }
        else if (incrementarIsOn)
        {
            IncrementarVelocidad();
        }
    }

    public void ReducirVelocidad()
    {
        if (reducirIsOn)
        {
            speed = 1;
            ReducirDuracion -= Time.deltaTime;
            movement.z = speed;

            if (ReducirDuracion <= 0)
            {
                ReducirDuracion = 5;
                reducirIsOn = false;
            }
        }
    }

    public void IncrementarVelocidad()
    {
        if (incrementarIsOn)
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

    // Método para colisiones con PowerUps y Monedas
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpRedicir"))
        {
            reducirIsOn = true;
        }

        if (other.CompareTag("PowerUpIncrementar"))
        {
            incrementarIsOn = true;
        }

        if (other.CompareTag("coin"))
        {
            // Lógica para recolectar la moneda
            Debug.Log("Moneda recolectada");
            GameManager.GetInstance().SumarPuntos(1);
            other.gameObject.SetActive(false);  // Desactivar la moneda
        }
    }
}
