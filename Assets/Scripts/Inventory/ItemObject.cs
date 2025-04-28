using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemDAta itemDAta;

    public void onHandledPickUp(){
        InventorySystem.Instance.Add(itemDAta);
        Destroy(gameObject);
    }

    private void OggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            onHandledPickUp();
        }
    }
}
