public class AttributeArmorUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.armor;
        UpdateTextAttributes();
    }
}
