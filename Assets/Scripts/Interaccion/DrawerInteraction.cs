using UnityEngine;

public class DrawerInteraction : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    [Header("Movimiento del Cajón")]
    public float openDistance = 0.3f;
    public float openSpeed = 2f; 
    public Vector3 moveDirection = Vector3.forward;

    void Start()
    {
        initialPosition = transform.localPosition;
        targetPosition = initialPosition + moveDirection.normalized * openDistance;
    }

    public void Interact()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(MoveDrawer(targetPosition));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveDrawer(initialPosition));
        }

        isOpen = !isOpen;
    }

    private System.Collections.IEnumerator MoveDrawer(Vector3 endPosition)
    {
        Vector3 startPos = transform.localPosition;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.localPosition = Vector3.Lerp(startPos, endPosition, t);
            yield return null;
        }

        transform.localPosition = endPosition; 
    }
}
