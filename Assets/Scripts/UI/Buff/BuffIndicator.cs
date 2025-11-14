using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffIndicator : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;

    public void Display(Item item) {
        SetIcon(item.Icon);
        StartCoroutine(ShowDurationEffect(item));
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    private IEnumerator ShowDurationEffect(Item item) {
        float _time = item.Duration;

        while (_time > 0) {
            _time -= Time.deltaTime;
            _border.fillAmount = _time / item.Duration;
            yield return null;
        }

        EventManager.TakeAwayItemEventHandler(item);
        _border.fillAmount = 1;
        gameObject.SetActive(false);
    }
}
