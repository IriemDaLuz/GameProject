using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class ShowInteractionUI : MonoBehaviour
{
    [Header("UI a mostrar")]
    public TextMeshProUGUI interactionText;   
    public GameObject iconObject;           

    private void Start()
    {
        if (interactionText != null)
            interactionText.enabled = false;

        if (iconObject != null)
            iconObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactuable"))
        {
            if (interactionText != null)
                interactionText.enabled = true;

            if (iconObject != null)
                iconObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactuable"))
        {
            if (interactionText != null)
                interactionText.enabled = false;

            if (iconObject != null)
                iconObject.SetActive(false);
        }
    }
}
