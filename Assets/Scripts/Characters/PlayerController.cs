using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : CharacterController {
    private Rigidbody rigidBody;
    private InputControls inputControls;
    private Vector3 lookDelta = Vector3.zero;
    
    [Space, SerializeField] private float lookSensitivity = 10f;
    [SerializeField] private float lerpSpeed = .5f;
    [SerializeField] private float minLookYClamp = -70f;
    [SerializeField] private float maxLookYClamp = 90f;
    
    [Space, SerializeField] private Transform eyesTransform;

    private void Awake() {
        inputControls = new InputControls();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        inputControls.Player.Enable();
        inputControls.Player.Look.performed += ReadLookDelta;
    }

    private void OnDisable() {
        inputControls.Player.Look.performed -= ReadLookDelta;
        inputControls.Player.Disable();
    }

    private void ReadLookDelta(InputAction.CallbackContext context) {
        Vector2 delta = context.ReadValue<Vector2>();
        lookDelta.x = Mathf.Clamp(lookDelta.x - delta.y * lookSensitivity * Time.deltaTime, minLookYClamp, maxLookYClamp);
        lookDelta.y += delta.x * lookSensitivity * Time.deltaTime;
    }

    protected override void Look() {
        Quaternion newRotation = Quaternion.Euler(lookDelta.x, 0, 0);
        eyesTransform.localRotation = Quaternion.Slerp(eyesTransform.localRotation, newRotation, lerpSpeed);
        newRotation = Quaternion.Euler(0, lookDelta.y, 0);
        rigidBody.MoveRotation(newRotation);
    }
}