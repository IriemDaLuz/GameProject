using UnityEngine;

public class ItemObject : MonoBehaviour
{
public GameObject objectToActivateOnPickup;

    public InventoryItemDAta itemDAta;

    public void Pickup()
{
    InventorySystem.Instance.Add(itemDAta);
    PickupUIManager.Instance.HideText();

    if (objectToActivateOnPickup != null)
    {
        objectToActivateOnPickup.SetActive(true);
    }

    Destroy(gameObject);
}

}
