using System.Collections;
using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _value;
    [SerializeField]
    private Animator _animator;

    public void Enable(float value) {
        _value.text = $"{string.Format("{0:0.0}", value)}";
        gameObject.SetActive(true);
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation() {
        yield return new WaitForSeconds(1f);
        _animator.Play("DamageView");
    }
}
