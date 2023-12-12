using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

    [SerializeField] private Canvas canvas;
    public RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    private Vector2 startPosition;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("BeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("EndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        ItemSlot droppedSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>();
        if (droppedSlot == null && rectTransform.anchoredPosition != startPosition) {
            rectTransform.anchoredPosition = startPosition;
         }
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("PointerDown");
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
    }
}



