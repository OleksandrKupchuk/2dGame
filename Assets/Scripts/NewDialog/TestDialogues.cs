using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestDialogues {
    [SerializeField]
    private List<DialogData> dialoguesData;

    public List<DialogData> DialoguesData => dialoguesData;
}
