using UnityEngine;
using UnityEngine.InputSystem; // Required for the new input system

public class PlayerCam : MonoBehaviour
{
    [Header("Sensitivity")]
    public float sensX = 200f;
    public float sensY = 200f;

    [Header("References")]
    public Transform orientation; // Player body (rotates only left/right)

    private float xRotation;
    private float yRotation;
    private Vector2 lookInput; // stores mouse delta

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Apply mouse delta from the new input system
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera up/down
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotate player body left/right
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // Called automatically by PlayerInput when Look action is triggered
    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}

