using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float crouchSpeed = 1f;
    public float gravity = -9.81f;

    [Header("Agacharse")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchCameraOffset = -0.5f;

    [Header("Control")]
    public Transform playerCamera;
    public float mouseSensitivity = 1.5f;
    public float cameraPitch = 0f;
    public float minPitch = -75f;
    public float maxPitch = 75f;

    [Header("Estado")]
    public bool controlesActivos = true;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private Vector3 originalCameraPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalCameraPosition = playerCamera.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!controlesActivos) return;

        HandleMovement();
        HandleMouseLook();
        HandleCrouch();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            currentSpeed = runSpeed;
        else if (isCrouching)
            currentSpeed = crouchSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCrouch()
{
    if (Input.GetKeyDown(KeyCode.LeftControl))
    {
        isCrouching = true;
        controller.height = crouchHeight;
        controller.center = new Vector3(0, crouchHeight / 2f, 0); 
        playerCamera.localPosition = originalCameraPosition + new Vector3(0, crouchCameraOffset, 0);
    }
    else if (Input.GetKeyUp(KeyCode.LeftControl))
    {
        isCrouching = false;
        controller.height = normalHeight;
        controller.center = new Vector3(0, normalHeight / 2f, 0); 
        playerCamera.localPosition = originalCameraPosition;
    }
}

}
