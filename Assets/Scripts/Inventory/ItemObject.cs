using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemDAta itemDAta;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            PickupUIManager.Instance.ShowText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            PickupUIManager.Instance.HideText();
        }
    }

    public void Pickup()
    {
        InventorySystem.Instance.Add(itemDAta);
        PickupUIManager.Instance.HideText();
        Destroy(gameObject);
    }
}
