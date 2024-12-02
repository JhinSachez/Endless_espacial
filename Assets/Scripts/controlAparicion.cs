using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlAparicion : MonoBehaviour
{
public DistanceScore _distanceScore;
    
    public GameObject obejtosTierra1;
    public GameObject obejtosTierra2;
    public GameObject obejtosTierra3;
    public GameObject objetosCielo1;
    public GameObject objetosCielo2;
    public GameObject objetosCielo3;
   /* public GameObject objetosEspacio1;
    public GameObject objetosEspacio2;
    public GameObject objetosEspacio3;*/

    public bool isOnEarth = false;
    public bool isOnSky = false;
    public bool isOnSpace = false;
    private void Update()
    {
        if (_distanceScore.distance <= 1)
        {
            isOnEarth = true;
            isOnSky = false;
            isOnSpace = false;
        }
        
        if (_distanceScore.distance >= 10000)
        {
            isOnEarth = false;
            isOnSky = true;
            isOnSpace = false;

        }
        
        if(_distanceScore.distance >= 20000)
        {
            isOnEarth = true;
            isOnSky = false;
            isOnSpace = true;

        }

        if (isOnEarth == true)
        {
            obejtosTierra1.SetActive(true);
            obejtosTierra2.SetActive(true);
            obejtosTierra3.SetActive(true);
            
            objetosCielo1.SetActive(false);
            objetosCielo2.SetActive(false);
            objetosCielo3.SetActive(false);
            
           /* objetosEspacio1.SetActive(false);
            objetosEspacio2.SetActive(false);
            objetosEspacio3.SetActive(false);*/
        }
        if (isOnSky == true)
        {
            obejtosTierra1.SetActive(false);
            obejtosTierra2.SetActive(false);
            obejtosTierra3.SetActive(false);
            
            objetosCielo1.SetActive(true);
            objetosCielo2.SetActive(true);
            objetosCielo3.SetActive(true);
            
           /* objetosEspacio1.SetActive(false);
            objetosEspacio2.SetActive(false);
            objetosEspacio3.SetActive(false);*/
        }
        if (isOnSpace == true)
        {
            obejtosTierra1.SetActive(false);
            obejtosTierra2.SetActive(false);
            obejtosTierra3.SetActive(false);
            
            objetosCielo1.SetActive(false);
            objetosCielo2.SetActive(false);
            objetosCielo3.SetActive(false);
            
           /* objetosEspacio1.SetActive(true);
            objetosEspacio2.SetActive(true);
            objetosEspacio3.SetActive(true);*/
        }
    }
}
