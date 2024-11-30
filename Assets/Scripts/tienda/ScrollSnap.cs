using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    public ScrollRect scrollRect; // El ScrollRect del ScrollView
    public RectTransform content; // El contenedor "Content"
    public RectTransform[] items; // Los elementos hijos dentro del Content
    public float snapSpeed = 10f; // Velocidad de ajuste

    private bool isDragging = false;
    private Vector2 targetPosition;

    void Update()
    {
        if (!isDragging && content.anchoredPosition != targetPosition)
        {
            // Ajusta suavemente la posición al objetivo
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, snapSpeed * Time.deltaTime);
        }
    }

    public void OnDragStart()
    {
        isDragging = true; // Detecta cuándo el usuario comienza a arrastrar
    }

    public void OnDragEnd()
    {
        isDragging = false;

        // Encuentra el elemento más cercano y ajusta su posición
        float closestDistance = float.MaxValue;
        foreach (var item in items)
        {
            float distance = Mathf.Abs(item.localPosition.x - content.localPosition.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPosition = new Vector2(-item.localPosition.x, content.anchoredPosition.y);
            }
        }
    }
}
