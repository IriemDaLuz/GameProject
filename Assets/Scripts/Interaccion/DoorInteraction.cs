using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion openRotation;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    void Start()
    {
        initialRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void Interact()
    {
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(OpenDoor());
        }
    }

    private System.Collections.IEnumerator OpenDoor()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(initialRotation, openRotation, t);
            yield return null;
        }
    }
}
