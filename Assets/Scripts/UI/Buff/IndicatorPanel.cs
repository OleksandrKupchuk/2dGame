using System.Collections.Generic;
using UnityEngine;

public class IndicatorPanel : MonoBehaviour {
    private List<BuffIndicator> _buffIndicators = new List<BuffIndicator>();
    [SerializeField]
    private BuffIndicator _indicator;

    private void Awake() {
        CreateBuffIndicators();
    }

    private void CreateBuffIndicators() {
        for (int i = 0; i < 5; i++) {
            BuffIndicator _indicatorObject = Instantiate(_indicator);
            _indicatorObject.transform.SetParent(transform, false);
            _indicatorObject.gameObject.SetActive(false);
            _buffIndicators.Add(_indicatorObject);
        }
    }

    public void ShowBuffIndacator(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            foreach (var indicator in _buffIndicators) {
                if (!indicator.gameObject.activeSelf) {
                    indicator.gameObject.SetActive(true);
                    indicator.Display(attribute);
                    break;
                }
            }
        }
    }
}
