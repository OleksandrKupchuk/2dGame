using System.Collections;
using UnityEngine;

public class MarketRestock : Singleton<MarketRestock> {
    private float _timer;

    [SerializeField]
    private float _timeUntilUpdatingItemsInSecond;
    [SerializeField]
    private Market _market;

    public IEnumerator StartUpdatingItems() {
        Debug.Log("<color=blue>Market stock start updating</color>");
        _timer = _timeUntilUpdatingItemsInSecond;

        while (_timer > 0) {
            _timer -= Time.deltaTime;
            yield return null;
        }

        _market.IsMarketChecked = false;
        Debug.Log("<color=green>Market</color> stock updated");
    }
}
