using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UsageSlotView : SlotView {
    private InputAction _inputAction;

    [SerializeField]
    private Text _label;
    [SerializeField]
    private ItemToolTip _itemToolTip;

    private void Awake() {
        DragAndDrop.OnItemDragged += ChangeBorderColor;
        DragAndDrop.OnDragEnded += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemDragged -= ChangeBorderColor;
        DragAndDrop.OnDragEnded -= ResetBorderColor;
    }

    public override void PutItem(Item item) {
        _item = item;
        SetIcon();
    }

    public override void RemoveItem() {
        _item = null;
        SetIcon();
    }

    private void ChangeBorderColor(Item itemData) {
        if (IsCanPutItem(itemData)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    public override bool IsCanPutItem(Item item) {
        if (item == null || item.ItemCategory.Equals(ItemCategory.Usable)) {
            return true;
        }

        return false;
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
        //print("Usage slot reset border color");
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }

    private void Update() {
        if (IsEmpty) { return; }

        if (_inputAction.triggered) {
            _item.Use();
            RemoveItem();

            if (_itemToolTip.IsActive) {
                _itemToolTip.Hide();
            }
        }
    }

    public void SetInputAction(InputAction inputAction) {
        _inputAction = inputAction;
        InputBinding inputBinding = _inputAction.bindings[0];
        string buttonLabel = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _label.text = buttonLabel;
    }
}
