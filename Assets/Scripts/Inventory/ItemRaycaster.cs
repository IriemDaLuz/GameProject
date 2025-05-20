using UnityEngine;

public class ItemRaycaster : MonoBehaviour
{
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private LayerMask pickupLayer;

    private ItemObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            ItemObject item = hit.collider.GetComponent<ItemObject>();

            if (item != null)
            {
                currentTarget = item;
                PickupUIManager.Instance.ShowText();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentTarget.Pickup();
                    currentTarget = null;
                }

                return; 
            }
        }

        currentTarget = null;
        PickupUIManager.Instance.HideText();
    }
}
