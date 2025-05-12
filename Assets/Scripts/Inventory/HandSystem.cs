using UnityEngine;
using UnityEngine.UI;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance;

    public InventoryItemDAta leftHandItem;
    public InventoryItemDAta rightHandItem;

    public Image leftHandIcon;
    public Image rightHandIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void AssignToLeftHand(InventoryItemDAta itemData)
    {
        leftHandItem = itemData;
        leftHandIcon.sprite = itemData.itemIcon;
        leftHandIcon.enabled = true;
    }

    public void AssignToRightHand(InventoryItemDAta itemData)
    {
        rightHandItem = itemData;
        rightHandIcon.sprite = itemData.itemIcon;
        rightHandIcon.enabled = true;
    }

    public bool IsInHand(string itemId)
    {
        return (leftHandItem != null && leftHandItem.id == itemId) ||
               (rightHandItem != null && rightHandItem.id == itemId);
    }
}
