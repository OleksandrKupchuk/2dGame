using UnityEngine;

public class PickUpController : MonoBehaviour {
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private QuestController _questController;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("DialogAction item king")) {
            print("pickup quest item");
            _questController.AddQuestItem(collision.gameObject);
        }

        if (collision.transform.TryGetComponent(out ItemView itemView)) {
            if (_inventory.TryAddItem(itemView.ItemData)) {
                Debug.Log("Pickup item = " + itemView.name);
                Destroy(itemView.gameObject);
            }
        }
    }
}
