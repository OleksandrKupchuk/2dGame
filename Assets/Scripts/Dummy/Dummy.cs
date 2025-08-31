using UnityEngine;

public class Dummy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Weapon weapon)) {
            Debug.Log("Hit by damage: " + weapon);
        }
    }
}
