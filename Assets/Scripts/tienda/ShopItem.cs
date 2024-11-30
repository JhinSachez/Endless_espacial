using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
        [Header("UI Elements")]
    [SerializeField] private Button buyButton;  // Botón de compra
    [SerializeField] private Button equipButton;  // Botón de equipar
    [SerializeField] private TMP_Text itemPriceText;  // Texto para el precio
   // [SerializeField] private TMP_Text itemNameText;  // Texto para el nombre del ítem

    [Header("Item Data")]
    [SerializeField] private int itemPrice;  // Precio del ítem
    [SerializeField] private string itemID;  // Identificador único del ítem (usado en PlayerPrefs)

    private void Start()
    {
        // Verificar si el ítem ya ha sido comprado
        if (IsItemPurchased())
        {
            buyButton.gameObject.SetActive(false);  // Desactivar el botón de compra
            equipButton.gameObject.SetActive(true);  // Activar el botón de equipar
        }
        else
        {
            buyButton.gameObject.SetActive(true);  // Activar el botón de compra
            equipButton.gameObject.SetActive(false);  // Desactivar el botón de equipar
        }

        // Asignar texto del precio
        itemPriceText.text = "$" + itemPrice.ToString();

        // Asignar evento para el botón de compra
        buyButton.onClick.AddListener(BuyItem);
    }

    // Función para comprar el ítem
    private void BuyItem()
    {
        // Verificar si el jugador tiene suficiente dinero
        if (GameManager.GetInstance().ObtenerMonedas() >= itemPrice)
        {
            // Descontar el dinero y registrar la compra
            GameManager.GetInstance().AgregarMonedas(-itemPrice);
            MarkItemAsPurchased();  // Marcar el ítem como comprado

            // Actualizar UI
            buyButton.gameObject.SetActive(false);  // Desactivar el botón de compra
            equipButton.gameObject.SetActive(true);  // Activar el botón de equipar
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar este ítem.");
        }
    }

    // Función para marcar el ítem como comprado
    private void MarkItemAsPurchased()
    {
        PlayerPrefs.SetInt(itemID, 1);  // Guardar que el ítem ha sido comprado
        PlayerPrefs.Save();
    }

    // Función para verificar si el ítem ha sido comprado
    private bool IsItemPurchased()
    {
        return PlayerPrefs.GetInt(itemID, 0) == 1;  // Si el valor guardado es 1, el ítem ya ha sido comprado
    }
}
