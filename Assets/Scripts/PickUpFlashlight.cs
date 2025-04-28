using UnityEngine;

public class PickUpFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashLightOnPlayer; 
    [SerializeField] private GameObject pickUpText; 
    private bool isPlayerInRange = false; 

    void Start()
    {
        flashLightOnPlayer.SetActive(false);
        pickUpText.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            flashLightOnPlayer.SetActive(true);
            pickUpText.SetActive(false); 
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUpText.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
