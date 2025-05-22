using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemDAta itemDAta;

    public void Pickup()
{
    InventorySystem.Instance.Add(itemDAta);
    PickupUIManager.Instance.HideText();

    if (!HandSystem.Instance.IsInHand(itemDAta.id))
    {
        if (HandSystem.Instance.rightHandItem == null)
            HandSystem.Instance.AssignToRightHand(itemDAta);
        else if (HandSystem.Instance.leftHandItem == null)
            HandSystem.Instance.AssignToLeftHand(itemDAta);
    }

    Destroy(gameObject);
}

}
