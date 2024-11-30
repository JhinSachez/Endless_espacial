using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
/* public int skinIndex;  // El índice de la skin que este botón representa
 public Button buyButton;  // El botón de comprar
 public Button equipButton;  // El botón de equipar

 private void Start()
 {
     buyButton.onClick.AddListener(OnBuyButtonClicked);
     equipButton.onClick.AddListener(OnEquipButtonClicked);

     // Si la skin ya fue comprada, el botón de equipar debe estar activo
     if (PlayerPrefs.GetInt("SkinPurchased" + skinIndex, 0) == 1)
     {
         buyButton.gameObject.SetActive(false);
         equipButton.gameObject.SetActive(true);
     }
 }

 // Método para cuando se compra una skin
 private void OnBuyButtonClicked()
 {
     int playerCoins = GameManager.GetInstance().ObtenerMonedas();
     int skinCost = 100;  // Ajusta el costo de la skin según sea necesario

     if (playerCoins >= skinCost)
     {
         // Resta las monedas del jugador
         GameManager.GetInstance().AgregarMonedas(-skinCost);

         // Marca la skin como comprada
         PlayerPrefs.SetInt("SkinPurchased" + skinIndex, 1);
         PlayerPrefs.Save();

         // Desactiva el botón de compra y activa el de equipar
         buyButton.gameObject.SetActive(false);
         equipButton.gameObject.SetActive(true);
     }
 }

 // Método para equipar la skin
 private void OnEquipButtonClicked()
 {
     // Asume que el SkinManager es persistente entre escenas
     SkinManager skinManager = FindObjectOfType<SkinManager>();
     if (skinManager != null)
     {
         skinManager.SetSkin(skinIndex);  // Cambiar la skin del jugador
     }
 }*/
}
