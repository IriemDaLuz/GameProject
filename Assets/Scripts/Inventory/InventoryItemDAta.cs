using UnityEngine;

[CreateAssetMenu(fileName ="Inventory Item Data", menuName ="Inventory System/Create Item", order = 0)]
public class InventoryItemDAta : ScriptableObject
{
    public string id;
    public string itenName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
