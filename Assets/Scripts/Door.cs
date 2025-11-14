using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteracvite {
    public void Interact() {
        SceneManager.LoadScene("Tavern");
    }
}
