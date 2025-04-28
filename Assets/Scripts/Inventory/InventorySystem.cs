using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    private Dictionary<InventoryItemDAta, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemDAta, InventoryItem>();   
        Instance= this;
    }
    
    public void Add(InventoryItemDAta itemDAta){
        if (_itemDictionary.TryGetValue(itemDAta , out InventoryItem value)){
            Debug.Log("SUMAR STACK EN ITEM");
            value.AddStack();
        } else {
            Debug.Log("AGREGAR UN NUEVO ITEM");
            InventoryItem newItem = new InventoryItem(itemDAta);
            inventory.Add(newItem);
            _itemDictionary.Add(itemDAta, newItem);
        }
    }

    public void Remove(InventoryItemDAta itemDAta){
        if (_itemDictionary.TryGetValue(itemDAta, out InventoryItem value)){
            value.RemoveFromStack();

            if (value.stackSize == 0){
                inventory.Remove(value);
                _itemDictionary.Remove(itemDAta);
            }
        }
    }
}
