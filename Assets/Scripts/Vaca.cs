using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vaca : MonoBehaviour
{
    public Vector3 direction = Vector3.left;
    public float speed = 5f;
    public float lifetime = 10f;
    public int puntos = 0;
    public UIController uiController;
    private bool yaColisionado = false; 

    void Start()
    {
        // Busca automáticamente el UIController en la escena
        uiController = FindObjectOfType<UIController>();

        // Asegúrate de que existe un UIController
        if (uiController == null)
        {
            Debug.LogError("No se encontró ningún UIController en la escena.");
        }
        
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !yaColisionado)
        {
            yaColisionado = true;
            GameManager.GetInstance().SumarPuntos(1);
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        // Mueve el objeto en la dirección especificada a la velocidad determinada
        transform.Translate(direction * speed * Time.deltaTime);

    }
}
