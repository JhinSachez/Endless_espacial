using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject itemPrefab; // Prefab del �tem que se mostrar� en la tienda.
    public Transform contentPanel; // Panel de contenido dentro del Scroll View.

    [Header("Shop Items List")]
    public List<ShopItem> shopItems = new List<ShopItem>(); // Lista de �tems que estar�n en la tienda.

    private void Start()
    {
        PopulateShop();
    }

    // M�todo para poblar la tienda con �tems.
    private void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            // Crear una instancia del prefab en el panel de contenido.
            GameObject newItem = Instantiate(itemPrefab, contentPanel);

            // Configurar el nombre, precio e imagen del �tem.
            newItem.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            newItem.transform.Find("ItemPrice").GetComponent<Text>().text = "$" + item.itemPrice.ToString("F2");
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage;

            // Agregar la funcionalidad de compra al bot�n.
            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    // M�todo que se llama cuando el jugador compra un �tem.
    private void BuyItem(ShopItem item)
    {
        // Aqu� puedes a�adir la l�gica de compra, como descontar el dinero, a�adir el �tem al inventario, etc.
        Debug.Log("Comprado: " + item.itemName + " por $" + item.itemPrice);
    }
}

// Clase que define los datos de cada �tem de la tienda.
[System.Serializable]
public class ShopItem
{
    public string itemName;
    public float itemPrice;
    public Sprite itemImage;
}

