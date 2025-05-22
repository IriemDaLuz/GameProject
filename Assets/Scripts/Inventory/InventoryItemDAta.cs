using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item Data", menuName = "Inventory System/Create Item", order = 0)]
public class InventoryItemDAta : ScriptableObject
{
    public bool activaAlRecoger = true;
    public string id;
    public string itenName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    [Header("Ajustes en Mano")]
public Vector3 handRotationOffset = Vector3.zero;
public Vector3 handScale = Vector3.one;

}
