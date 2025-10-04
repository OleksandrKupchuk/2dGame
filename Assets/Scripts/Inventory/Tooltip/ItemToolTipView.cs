using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTipView : MonoBehaviour {
    private RectTransform _backgroundRectTransform;
    private List<AttributeToolTip> _attributeTooltips = new List<AttributeToolTip>();
    private int _amountAttributeToolTips = 5;

    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private GameObject _containerAttributes;
    [SerializeField]
    private AttributeToolTip _attributePrefab;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private Text _priceValue;
    [SerializeField]
    private GameObject _containerDuration;
    [SerializeField]
    private Text _durationValue;

    public bool IsActive => _background.activeSelf;

    private void Awake() {
        _backgroundRectTransform = _background.GetComponent<RectTransform>();

        if (_background == null) {
            Debug.LogError($"Object 'background' is null");
        }
        if (_name == null) {
            Debug.LogError($"Object '_name' is null");
        }
        if (_description == null) {
            Debug.LogError($"Object '_description' is null");
        }

        _background.SetActive(false);
        CreateAttributeToolTips();
    }

    private void CreateAttributeToolTips() {
        for (int i = 0; i < _amountAttributeToolTips; i++) {
            AttributeToolTip _attributeTooltip = Instantiate(_attributePrefab, _containerAttributes.transform);
            _attributeTooltip.gameObject.SetActive(false);
            _attributeTooltips.Add(_attributeTooltip);
        }
    }

    public void Enable(Item item, RectTransform rectTransform) {
        //print("GetView tool tip");
        _name.text = item.Name;
        _description.text = item.Description;
        _priceValue.text = item.Price.ToString();

        if (item.ItemType.Equals(ItemType.Usable)) {
            if (item.Duration > 0) {
                ShowDuration(item);
            }
            else {
                HideDuration();
            }
        }
        else {
            HideDuration();
        }

        _rectTransform.position = new Vector2(rectTransform.position.x, rectTransform.position.y);
        SetAndEnableAttributes(item);
        EnableBackground();
        StartCoroutine(SetPosition(rectTransform));
    }

    private void ShowDuration(Item item) {
        _durationValue.text = item.Duration.ToString();
        _containerDuration.SetActive(true);
    }

    private void HideDuration() {
        _containerDuration.SetActive(false);
    }

    private void SetAndEnableAttributes(Item itemData) {
        for (int i = 0; i < itemData.Attributes.Count; i++) {
            _attributeTooltips[i].Set(itemData.Attributes[i].Icon, itemData.Attributes[i].GetValue());
            _attributeTooltips[i].gameObject.SetActive(true);
        }
    }

    private void EnableBackground() {
        _background.SetActive(true);
    }

    private IEnumerator SetPosition(RectTransform rectTransform) {
        yield return StartCoroutine(WaitForEnableBackground());
        float _halfSlotHeight = rectTransform.rect.height / 2;
        float _halfToolTipHeight = _backgroundRectTransform.rect.height / 2;
        float _spaceBetweenToolTipAndCell = 5f;
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + _halfSlotHeight + _halfToolTipHeight + _spaceBetweenToolTipAndCell);
    }

    public void DisableBackground() {
        foreach (AttributeToolTip attributeTooltip in _attributeTooltips) {
            attributeTooltip.gameObject.SetActive(false);
        }

        _background.SetActive(false);
    }

    private IEnumerator WaitForEnableBackground() {
        yield return new WaitUntil(() => _background.activeSelf == true);
    }
}
