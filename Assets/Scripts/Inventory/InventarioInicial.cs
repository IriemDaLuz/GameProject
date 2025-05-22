using UnityEngine;

public class InventarioInicial : MonoBehaviour
{
    [SerializeField] private InventoryItemDAta pulseraInicial;

    void Start()
    {
        InventorySystem.Instance.Add(pulseraInicial);
    }
}
