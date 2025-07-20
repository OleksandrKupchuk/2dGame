using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UsageSlotView : SlotView {
    private InputAction _inputAction;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    private Text _label;

    private void Awake() {
        DragAndDrop.OnItemDragged += ChangeBorderColor;
        DragAndDrop.OnDragEnded += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemDragged -= ChangeBorderColor;
        DragAndDrop.OnDragEnded -= ResetBorderColor;
    }

    public override void PutItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon();
    }

    public override void RemoveItem() {
        _itemData = null;
        SetIcon();
    }

    private bool CanUseItem(ItemData itemData) {
        if (itemData is UsableItemData) {
            return true;
        }

        return false;
    }

    public override bool IsCanPutItem(ItemData itemData) {
        if (itemData == null || itemData is UsableItemData) {
            return true;
        }

        return false;
    }

    private void ChangeBorderColor(ItemData itemData) {
        if (IsCanPutItem(itemData)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
        //print("Usage slot reset border color");
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }

    //потрібно переробити
    //private void Update() {
    //    if (!HasItem) { return; }

    //    if (_inputAction.triggered) {
    //        UseItem();
    //    }
    //}

    public void SetInputAction(InputAction inputAction) {
        _inputAction = inputAction;
        InputBinding inputBinding = _inputAction.bindings[0];
        string buttonLabel = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _label.text = buttonLabel;
    }
}
