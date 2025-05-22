using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Transform slotParent;       
    [SerializeField] private GameObject slotPrefab;      

    private void Awake()
    {
        InventorySystem.Instance.onInventoryChangedEventCallback += RefreshUI;
    }

    private void OnDestroy()
    {
        InventorySystem.Instance.onInventoryChangedEventCallback -= RefreshUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in InventorySystem.Instance.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<ItemSlot>().Set(item);
        }
    }
}
