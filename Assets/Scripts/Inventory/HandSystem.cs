using UnityEngine;
using UnityEngine.UI;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance;

    public InventoryItemDAta leftHandItem;
    public InventoryItemDAta rightHandItem;

    public Image leftHandIcon;
    public Image rightHandIcon;

    public Transform leftHandHolder;
    public Transform rightHandHolder;

    private GameObject leftHandObject;
    private GameObject rightHandObject;

    private void Awake()
    {
        Instance = this;
    }

    public void AssignToLeftHand(InventoryItemDAta itemData)
    {
        if (rightHandItem != null && rightHandItem.id == itemData?.id)
        {
            AssignToRightHand(null); 
        }

        leftHandItem = itemData;

        if (itemData != null)
        {
            leftHandIcon.sprite = itemData.itemIcon;
            leftHandIcon.enabled = true;
        }
        else
        {
            leftHandIcon.enabled = false;
        }

        UpdateHandObject(ref leftHandObject, leftHandHolder, itemData);
    }

    public void AssignToRightHand(InventoryItemDAta itemData)
    {
        if (leftHandItem != null && leftHandItem.id == itemData?.id)
        {
            AssignToLeftHand(null);
        }

        rightHandItem = itemData;

        if (itemData != null)
        {
            rightHandIcon.sprite = itemData.itemIcon;
            rightHandIcon.enabled = true;
        }
        else
        {
            rightHandIcon.enabled = false;
        }

        UpdateHandObject(ref rightHandObject, rightHandHolder, itemData);
    }

    private void UpdateHandObject(ref GameObject currentObj, Transform holder, InventoryItemDAta itemData)
{
    if (currentObj != null)
    {
        Destroy(currentObj);
    }

    if (itemData != null && itemData.itemPrefab != null)
    {
        currentObj = Instantiate(itemData.itemPrefab, holder);

        currentObj.transform.localPosition = Vector3.zero; 
        currentObj.transform.localEulerAngles = itemData.handRotationOffset;
        currentObj.transform.localScale = itemData.handScale;
    }
}


    public bool IsInHand(string itemId)
    {
        return (leftHandItem != null && leftHandItem.id == itemId) ||
               (rightHandItem != null && rightHandItem.id == itemId);
    }
} 