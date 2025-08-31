using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    private RectTransform _rectTransform;
    private Transform _parent;
    private bool _isDropZone = false;

    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Inventory _inventory;

    [field: SerializeField]
    public SlotView SlotView { get; private set; }

    public static event Action OnDragStarted;
    public static event Action<Item> OnItemDragged;
    public static event Action OnDragEnded;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _parent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (SlotView.IsEmpty) { return; }
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
        OnItemDragged?.Invoke(SlotView.Item);
        print("OnDragStarted");
        OnDragStarted?.Invoke();
    }

    public void OnDrag(PointerEventData eventData) {
        if (SlotView.IsEmpty) { return; }
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (_isDropZone) {
            EventManager.OnItemDropHandler(SlotView.Item);
            ResetSlotPosition();
            print("Drop Item from Inventory");
        }
        else {
            ResetSlotPosition();
        }
    }

    public void SetDropZone(bool isDropZone) {
        _isDropZone = isDropZone;
    }

    private void ResetSlotPosition() {
        transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        OnDragEnded?.Invoke();
    }
}
