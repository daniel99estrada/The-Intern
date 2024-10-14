using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 movementInput;
    private Rigidbody rb;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += OnMovementPerformed;
        playerInputActions.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Z axes
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
