using UnityEngine;

public interface IInteractable
{
    void Interact();
}


public class InteractionDetector : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public GameObject InteractionUI; 
    private IInteractable currentInteractable;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                currentInteractable = interactObj;
                InteractionUI.SetActive(true); 

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }

                return; 
            }
        }

        currentInteractable = null;
        InteractionUI.SetActive(false);
    }
}
