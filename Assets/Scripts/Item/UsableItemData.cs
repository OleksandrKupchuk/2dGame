using UnityEngine;

[CreateAssetMenu(fileName = "UsableItemData", menuName = "ItemData/UsableItemData")]
public class UsableItemData : ItemData {
    [field: SerializeField]
    public float Duration { get; protected set; }

    public virtual void Use() {
        //EventManager.UseItemEventHandler(this);
        //Invoke(nameof(StartTimerDelay), Duration);
    }

    private void StartTimerDelay() {
        //EventManager.ActionItemOverEventHandler(this);
    }
}
