using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion initialRotation;
    public float openAngle = 90f;
    public float speed = 2f;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? Quaternion.Euler(0, openAngle, 0) * initialRotation : initialRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
}
