using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonedasTotalesUI : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCoinsText;  // Referencia al texto en la UI

    private void Start()
    {
        UpdateCoinsDisplay();  // Actualiza la UI al iniciar
    }

    private void UpdateCoinsDisplay()
    {
        int totalCoins = GameManager.GetInstance().ObtenerMonedas();
        totalCoinsText.text = "Vacas: " + totalCoins;
    }

    public void OnCoinsUpdated()
    {
        UpdateCoinsDisplay();
    }
}
