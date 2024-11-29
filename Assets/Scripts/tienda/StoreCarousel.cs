using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreCarousel : MonoBehaviour
{
     [Header("Carrusel Settings")]
    [SerializeField] private List<StoreItem> storeItems; // Lista de ítems de la tienda
    [SerializeField] private RectTransform contentBoxHorizontal; // Contenedor para los ítems
    [SerializeField] private StoreItemUI itemPrefab; // Prefab de los ítems
    [SerializeField] private ScrollRect scrollRect; // Componente de desplazamiento

    [Header("Indicators")]
    [SerializeField] private Transform indicatorParent; // Contenedor para los indicadores
    [SerializeField] private CarouselIndicators indicatorPrefab; // Prefab de indicadores
    private List<CarouselIndicators> _indicators = new List<CarouselIndicators>();

    [Header("Animation")]
    [SerializeField, Range(0.25f, 1f)] private float scrollDuration = 0.5f;
    [SerializeField] private AnimationCurve easeCurve;

    private int _currentIndex = 0;
    private Coroutine _scrollCoroutine;

    private void Start()
    {
        SetupCarousel();
    }

    private void SetupCarousel()
    {
        float itemWidth = itemPrefab.GetComponent<RectTransform>().rect.width; // Ancho del ítem
        float spacing = 20f; // Espaciado entre ítems
        float contentWidth = 0f; // Ancho total del contenedor
    
        for (int i = 0; i < storeItems.Count; i++)
        {
            // Crear el ítem visual
            var storeItemUI = Instantiate(itemPrefab, contentBoxHorizontal);
            storeItemUI.Setup(storeItems[i]);

            // Calcular la posición del ítem en el eje horizontal
            RectTransform itemRect = storeItemUI.GetComponent<RectTransform>();
            itemRect.anchoredPosition = new Vector2((itemWidth + spacing) * i, 0);
        
            // Incrementar el ancho total del contenedor
            contentWidth += itemWidth + spacing;

            // Crear el indicador correspondiente
            var indicator = Instantiate(indicatorPrefab, indicatorParent);
            indicator.Initialize(() => ScrollToSpecificIndex(i));
            _indicators.Add(indicator);
        }

        // Ajustar el tamaño del contenedor
        contentBoxHorizontal.sizeDelta = new Vector2(contentWidth, contentBoxHorizontal.sizeDelta.y);

        // Activar el primer indicador
        if (_indicators.Count > 0)
            _indicators[0].Activate(0.1f);
    }

    public void ScrollToNext()
    {
        ChangeCurrentIndex((_currentIndex + 1) % storeItems.Count);
    }

    public void ScrollToPrevious()
    {
        ChangeCurrentIndex((_currentIndex - 1 + storeItems.Count) % storeItems.Count);
    }

    private void ScrollToSpecificIndex(int index)
    {
        ChangeCurrentIndex(index);
    }

    private void ChangeCurrentIndex(int newIndex)
    {
        _indicators[_currentIndex].Deactivate(scrollDuration);
        _currentIndex = newIndex;
        _indicators[_currentIndex].Activate(scrollDuration);

        float targetHorizontalPosition = (float)_currentIndex / (storeItems.Count - 1);

        if (_scrollCoroutine != null)
            StopCoroutine(_scrollCoroutine);

        _scrollCoroutine = StartCoroutine(LerpToPosition(targetHorizontalPosition));
    }

    private IEnumerator LerpToPosition(float targetPosition)
    {
        float elapsedTime = 0f;
        float initialPosition = scrollRect.horizontalNormalizedPosition;

        while (elapsedTime <= scrollDuration)
        {
            float easedValue = easeCurve.Evaluate(elapsedTime / scrollDuration);
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(initialPosition, targetPosition, easedValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
