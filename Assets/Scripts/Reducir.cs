using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reducir : MonoBehaviour
{
    bool isOnPlay;
    public float Timer = 0;
    public float timeToDeactivated = 15;
   
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChange;
        OnGameStateChange(GameManager.GetInstance().currentGameState);
    }
    
    void OnGameStateChange(Game_State _gs)
    {
        isOnPlay = _gs == Game_State.Play;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayPowerUp();

            other.GetComponent<PlayerCollision>().particulasColisionPower();


            gameObject.SetActive(false);
           
        }

        if (other.CompareTag("Enemigo"))
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isOnPlay) return;
        
        Timer += Time.deltaTime;

        if (Timer > timeToDeactivated)
        {
            gameObject.SetActive(false);
        }
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 0.5f);  // Radio de búsqueda de 0.5 unidades

        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemigo"))
            {
                gameObject.SetActive(false);
                return;
            }
        }

    }

}
