using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour {
    private CreationItem _creationItem = new CreationItem();
    private List<ChestSlotView> _slots = new List<ChestSlotView>();

    [SerializeField]
    private ChestSlotView _slotView;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private ButtonClosePanels _buttonClosePanels;
    [SerializeField]
    private LayoutElement _layoutElement;

    [field: SerializeField]
    public int AmountSlots { get; private set; }

    private void Awake() {
        GenerateSlots();
        Chest.OnGetLoot += PutItems;
        Chest.OnOpen += Open;
        _buttonClosePanels.OnClosePanels += Close;

        Close();
    }

    private void OnDestroy() {
        Chest.OnGetLoot += PutItems;
        Chest.OnOpen -= Open;
        _buttonClosePanels.OnClosePanels -= Close;
    }

    private void GenerateSlots() {
        for (int i = 0; i < AmountSlots; i++) {
            ChestSlotView _slotViewObject = Instantiate(_slotView, _content);
            _slotViewObject.PutItem(null);
            _slots.Add(_slotViewObject);
        }
    }

    public void Open() {
        _layoutElement.ignoreLayout = false;
        _background.SetActive(true);
    }

    private void PutItems(List<Item> items) {
        for (int i = 0; i < AmountSlots; i++) {
            if (i < items.Count) {
                Item _item = _creationItem.GetCreatedItem(items[i]);
                _slots[i].PutItem(_item);
            }
        }
    }

    public void Close() {
        _layoutElement.ignoreLayout = true;
        _background.SetActive(false);
    }
}
