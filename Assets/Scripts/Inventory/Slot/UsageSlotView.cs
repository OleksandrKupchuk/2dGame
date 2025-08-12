using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UsageSlotView : SlotView {
    private InputAction _inputAction;
    [SerializeField]
    protected Image _border;
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
        if (item == null || item.ItemType.Equals(ItemType.Usable)) {
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
            EventManager.UseItemEventHandler(_item);
            StartCoroutine(StartTimerDelay(_item));
            RemoveItem();

            if (_itemToolTip.IsActive) {
                _itemToolTip.Hide();
            }
        }
    }

    private IEnumerator StartTimerDelay(Item item) {
        yield return new WaitForSeconds(item.Duration);
        EventManager.TakeAwayItemEventHandler(item);
    }

    public void SetInputAction(InputAction inputAction) {
        _inputAction = inputAction;
        InputBinding inputBinding = _inputAction.bindings[0];
        string buttonLabel = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _label.text = buttonLabel;
    }
}
