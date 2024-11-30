using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StoreCarousel : MonoBehaviour
{
        [Header("Parts Setup")]
    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>(); // Lista de StoreItem (ScriptableObject)
        
    [Space]
    [SerializeField] private ScrollRect scrollRect;
    
    [Space]
    [SerializeField] private RectTransform contentBoxHorizontal;
    [SerializeField] private StoreItemUI itemPrefab; // Prefab para los ítems
    private List<StoreItemUI> _storeItemUIs = new List<StoreItemUI>();
        
    [Space]
    [SerializeField] private Transform indicatorParent;
    [SerializeField] private CarouselIndicators indicatorPrefab;
    private List<CarouselIndicators> _indicators = new List<CarouselIndicators>();
    
    [Header("Animation Setup")]
    [SerializeField, Range(0.25f, 1f)] private float duration = 0.5f;
    [SerializeField] private AnimationCurve easeCurve;
    
    [Header("Auto Scroll Setup")]
    [SerializeField] private bool autoScroll = false;
    [SerializeField] private float autoScrollInterval = 5f;
    private float _autoScrollTimer;
    
    private int _currentIndex = 0;
    private Coroutine _scrollCoroutine;

    private void Reset()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
    }

    private void Start()
    {
        float itemSpacing = 20f; // Ajusta el valor de espaciado según tus necesidades
        float contentWidth = 0f; // Esta variable determinará el tamaño total del contenedor

        foreach (var item in storeItems)
        {
            // Instanciamos el prefab para cada StoreItem
            StoreItemUI storeItemUI = Instantiate(itemPrefab, contentBoxHorizontal);
            storeItemUI.Setup(item);

            // Calculamos el ancho de cada ítem
            RectTransform itemRect = storeItemUI.GetComponent<RectTransform>();
            float itemWidth = itemRect.rect.width;
        
            // Posicionamos el ítem
            itemRect.anchoredPosition = new Vector2(contentWidth, 0);
            contentWidth += itemWidth + itemSpacing;

            // Añadimos un indicador para cada ítem
            var indicator = Instantiate(indicatorPrefab, indicatorParent);
            int index = storeItems.IndexOf(item);
            indicator.Initialize(() => ScrollToSpecificIndex(index));
            _indicators.Add(indicator);
        }

        // Ajustamos el tamaño del contenedor horizontal
        contentBoxHorizontal.sizeDelta = new Vector2(contentWidth, contentBoxHorizontal.sizeDelta.y);

        // Activar el primer indicador
        if (_indicators.Count > 0)
        {
            _indicators[0].Activate(0.1f);
        }
    }

    private void ClearCurrentIndex()
    {
        _indicators[_currentIndex].Deactivate(duration);
    }

    private void ScrollToSpecificIndex(int index)
    {
        ClearCurrentIndex();
        ScrollTo(index);
    }

    public void ScrollToNext()
    {
        ClearCurrentIndex();
        _currentIndex = (_currentIndex + 1) % _storeItemUIs.Count;
        ScrollTo(_currentIndex);
    }

    public void ScrollToPrevious()
    {
        ClearCurrentIndex();
        _currentIndex = (_currentIndex - 1 + _storeItemUIs.Count) % _storeItemUIs.Count;
        ScrollTo(_currentIndex);
    }

    private void ScrollTo(int index)
    {
        _currentIndex = index;
        _autoScrollTimer = autoScrollInterval;
        float targetHorizontalPosition = (float)_currentIndex / (_storeItemUIs.Count - 1);

        if (_scrollCoroutine != null)
            StopCoroutine(_scrollCoroutine);

        _scrollCoroutine = StartCoroutine(LerpToPos(targetHorizontalPosition));

        // Aquí ya no se necesita la descripción, así que la hemos eliminado.
        _indicators[_currentIndex].Activate(duration);
    }

    private IEnumerator LerpToPos(float targetHorizontalPosition)
    {  
        float elapsedTime = 0f;
        float initialPos = scrollRect.horizontalNormalizedPosition;

        if (duration > 0)
        {
            while (elapsedTime <= duration)
            {
                float easeValue = easeCurve.Evaluate(elapsedTime / duration);

                float newPosition = Mathf.Lerp(initialPos, targetHorizontalPosition, easeValue);

                scrollRect.horizontalNormalizedPosition = newPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        
        scrollRect.horizontalNormalizedPosition = targetHorizontalPosition;
    }

    private void Update()
    {
        if (!autoScroll) 
            return;
        
        _autoScrollTimer -= Time.deltaTime;
        if (_autoScrollTimer <= 0)
        {
            ScrollToNext();
            _autoScrollTimer = autoScrollInterval;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (data.delta.x != 0)
        {
            if (data.delta.x > 0)
                ScrollToPrevious();
            else if (data.delta.x < 0)
                ScrollToNext();
        }
        else
            ScrollToSpecificIndex(_currentIndex);
    }
}
