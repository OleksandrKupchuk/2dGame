using UnityEngine;

public class ResourcesPath : MonoBehaviour
{
    //Assets/Resources/Sprites/Inventory/ItemToolTip/Speed.png
    //Folders
    public static readonly string FolderPrefabs = "Prefabs/";
    public static readonly string FolderFieldOfView = FolderPrefabs + "FieldOfView/";
    public static readonly string FolderTooltip = "Sprites/Attributes/";
    public static readonly string FolderPlayerConfig = "Config/PlayerConfig/";

    //Prefabs
    public static readonly string FieldOfViewPrefab = FolderFieldOfView + "FieldOfView";
    public static readonly string FieldOfViewPrefabByMonkey = FolderFieldOfView + "FieldOfViewByMonkey";
}