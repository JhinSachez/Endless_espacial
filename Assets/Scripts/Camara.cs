using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject jugador;
        private Vector3 offset; 
        void Start()
        {
            offset = transform.position;
        }
    
        private void LateUpdate()
        {
            transform.position = new Vector3(offset.x, jugador.transform.position.y + offset.y, jugador.transform.position.z + offset.z  );
    
        }
}
