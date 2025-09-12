using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour {
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private TextMeshProUGUI _damage;
    [SerializeField]
    private float _minOffsetY;
    [SerializeField]
    private float _maxOffsetY;
    [SerializeField]
    private float _minOffsetX;
    [SerializeField]
    private float _maxOffsetX;
    [SerializeField]
    private float _animationDuration;

    public void ShowDamage(float damage, Color color) {
        _damage.color = color;
        _damage.text = $"{string.Format("{0:0.0}", damage)}";
        PlayAnimation();
    }

    public void PlayAnimation() {
        transform.localScale = new Vector2(0.7f, 0.7f);

        float _axisY = Random.Range(_minOffsetY, _maxOffsetY);
        float _axisX = Random.Range(_minOffsetX, _maxOffsetX);
        Debug.Log($"axisY = {_axisY}, axisX = {_axisX}");

        Vector3[] _path = new Vector3[]
        {
            _rectTransform.localPosition,
            new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y + _axisY, 0f),
            new Vector3(_rectTransform.localPosition.x + _axisX, _rectTransform.localPosition.y + _axisY, 0f)
        };

        _path.ToList().ForEach(point => Debug.Log($"x = {point.x}, y = {point.y}"));

        Sequence _sequence = DOTween.Sequence();

        _sequence.Append(_rectTransform.DOLocalPath(_path, _animationDuration, PathType.CatmullRom, PathMode.TopDown2D)
            .SetEase(Ease.OutExpo));

        _sequence.Join(_rectTransform.DOScale(new Vector3(1.2f, 1.2f, 0f), _animationDuration)
            .SetEase(Ease.OutBounce));

        _sequence.OnComplete(() => gameObject.SetActive(false));
    }
}
