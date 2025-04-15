using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogs", menuName = "NpcDialogues/NewDialogs")]
public class NpcDialogues : ScriptableObject {
    [SerializeField]
    private string _npcName;
    [SerializeField]
    private List<DialogData> _dialoguesData = new List<DialogData>();

    public List<DialogData> DialoguesData => _dialoguesData;

    //public void OnEnable() {
    //    if (DialoguesData == null) {
    //        return;
    //    }

    //    foreach (var dialog in DialoguesData) {
    //        dialog.IsDialogExpired = false;
    //    }

    //}
}
