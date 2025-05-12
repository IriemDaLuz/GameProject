using UnityEngine;
using TMPro;

public class PickupUIManager : MonoBehaviour
{
    public static PickupUIManager Instance;

    [SerializeField] private GameObject pickupText;

    private void Awake()
    {
        Instance = this;
        pickupText.SetActive(false);
    }

    public void ShowText()
    {
        pickupText.SetActive(true);
    }

    public void HideText()
    {
        pickupText.SetActive(false);
    }
}
