using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    // Cambiamos la clave del diccionario de InventoryItemDAta a string (el ID)
    private Dictionary<string, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory;

    public delegate void onInventoryChangedEvent();
    public event onInventoryChangedEvent onInventoryChangedEventCallback;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<string, InventoryItem>();
        Instance = this;
    }

    public void Add(InventoryItemDAta itemDAta)
    {
        if (_itemDictionary.TryGetValue(itemDAta.id, out InventoryItem value))
        {
            Debug.Log("SUMAR STACK EN ITEM");
            value.AddStack();
        }
        else
        {
            Debug.Log("AGREGAR UN NUEVO ITEM");
            InventoryItem newItem = new InventoryItem(itemDAta);
            inventory.Add(newItem);
            _itemDictionary.Add(itemDAta.id, newItem);
        }

        onInventoryChangedEventCallback?.Invoke();
    }

    public void Remove(InventoryItemDAta itemDAta)
    {
        if (_itemDictionary.TryGetValue(itemDAta.id, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(itemDAta.id);
            }

            onInventoryChangedEventCallback?.Invoke();
        }
    }
}
