using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour {
    private RectTransform _backgroundRectTransform;
    private List<AttributeTooltip> _attributeTooltips = new List<AttributeTooltip>();

    [SerializeField]
    private Image _imageBackground;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private AttributeTooltip _attributePrefab;

    private void Awake() {
        _backgroundRectTransform = _background.GetComponent<RectTransform>();

        if (_background == null) {
            Debug.LogError("Object 'background' is null");
        }

        _imageBackground.enabled = false;
        CreateAttributes();
    }

    private void CreateAttributes() {
        for (int i = 0; i < 5; i++) {
            AttributeTooltip _attributeTooltip = Instantiate(_attributePrefab);
            _attributeTooltip.transform.SetParent(_background.transform);
            _attributeTooltip.transform.localScale = new Vector3(1, 1, 1);
            _attributeTooltip.gameObject.SetActive(false);
            _attributeTooltips.Add(_attributeTooltip);
        }
    }

    public void ShowAttributes(Item item, Vector2 positionCell, float heightCell) {
        DisableAttributes();
        item.ShowTooltip(_attributeTooltips);

        EnableImageBackground();
        SetPosition(positionCell, heightCell);
    }

    public void DisableAttributes() {
        foreach (AttributeTooltip attributeTooltip in _attributeTooltips) {
            attributeTooltip.gameObject.SetActive(false);
        }

        DisableImageBackground();
    }

    private void SetPosition(Vector2 pos, float heightCell) {
        _background.transform.position = pos;
        float _spaceBetweenTooltipAndCellInPixel = 10;
        float _heightInPixel = (_backgroundRectTransform.rect.height + heightCell) / 2 + _spaceBetweenTooltipAndCellInPixel;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _backgroundRectTransform.anchoredPosition.y + _heightInPixel);
    }

    private void EnableImageBackground() {
        _imageBackground.enabled = true;
    }

    private void DisableImageBackground() {
        _imageBackground.enabled = false;
    }
}