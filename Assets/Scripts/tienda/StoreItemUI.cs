using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private Button purchaseButton;

    private StoreItem itemData;

    public void Setup(StoreItem item)
    {
        itemData = item;
        itemNameText.text = item.Name;
        itemPriceText.text = $"${item.Price}";
        itemIconImage.sprite = item.Icon;

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        Debug.Log($"Purchased: {itemData.Name} for ${itemData.Price}");
        // Aquí puedes implementar la lógica de reducción de moneda o inventario.
    }
}
