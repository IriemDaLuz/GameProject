using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void Interact()
    {
        if (!isOpen)
        {
            StartCoroutine(RotateDoor(targetRotation));
        }
        else
        {
            StartCoroutine(RotateDoor(initialRotation));
        }

        isOpen = !isOpen;
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion endRotation)
    {
        Quaternion startRotation = transform.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }
}
