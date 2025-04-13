using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFloor", menuName = "NewFloor/NewFloor")]
public class Floors : ScriptableObject {
    [SerializeField]
    private List<Room> _rooms = new List<Room>();
}
