using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            Debug.Log("OnPointerEnter");
            dragAndDrop.SetDropZone(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            Debug.Log("OnPointerExit");
            dragAndDrop.SetDropZone(false);
        }
    }
}
