using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public GameObject coinDetectorObj;

    void Start()
    {
        coinDetectorObj = GameObject.FindGameObjectWithTag("CoinDetector");
        coinDetectorObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(ActiveCoin());
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    IEnumerator ActiveCoin()
    {
        coinDetectorObj.SetActive(true);
        yield return new WaitForSeconds(10f);
        coinDetectorObj.SetActive(false);
    }
}
