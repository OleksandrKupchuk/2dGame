using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsView : MonoBehaviour {
    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Text _value;

    private void Awake() {
        EventManager.OnPriceUpdate += UpdateCoins;
    }

    private void OnDestroy() {
        EventManager.OnPriceUpdate -= UpdateCoins;
    }

    private void Start() {
        UpdateCoins();
    }

    public void UpdateCoins() {
        _value.text = "" + _playerConfig.Coins;
    }
}
