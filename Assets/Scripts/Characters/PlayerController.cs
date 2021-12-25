using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Character {
    private Rigidbody rigidBody;
    private InputControls inputControls;
    private Vector3 lookDelta = Vector3.zero;
    private Vector3 moveDelta = Vector3.zero;

    [Space, SerializeField] private float lookSensitivity = 10f;
    [SerializeField] private float lerpSpeed = .5f;
    [SerializeField] private float minLookYClamp = -70f;
    [SerializeField] private float maxLookYClamp = 90f;

    [Space, SerializeField] private Transform eyesTransform;
    [SerializeField] private Transform head;

    private void Awake() {
        inputControls = new InputControls();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.mass = mass;
        lookDelta = transform.localRotation.eulerAngles;
    }

    private void OnEnable() {
        inputControls.Player.Enable();
        inputControls.Player.Look.performed += ReadLookDelta;
        inputControls.Player.Move.performed += ReadMoveDelta;
    }

    private void OnDisable() {
        inputControls.Player.Look.performed -= ReadLookDelta;
        inputControls.Player.Move.performed -= ReadMoveDelta;
        inputControls.Player.Disable();
    }

    protected void FixedUpdate() {
        if (state != CharacterState.Dead) {
            base.FixedUpdate();

            if (inputControls.Player.Run.IsPressed()) {
                Run();
            }
            else {
                Walk();
            }
        }
    }

    private void ReadMoveDelta(InputAction.CallbackContext context) {
        if (state == CharacterState.Dead)
            return;
        
        Vector2 delta = context.ReadValue<Vector2>();
        moveDelta.x = delta.x;
        moveDelta.z = delta.y;
    }

    private void ReadLookDelta(InputAction.CallbackContext context) {
        Vector2 delta = context.ReadValue<Vector2>();
        lookDelta.x = Mathf.Clamp(lookDelta.x - delta.y * lookSensitivity * Time.deltaTime, minLookYClamp,
            maxLookYClamp);
        lookDelta.y += delta.x * lookSensitivity * Time.deltaTime;
    }

    protected override void Walk() {
        Vector3 walkForce = Vector3.zero;
        walkForce.x = moveDelta.x * mass * walkSpeed * Time.fixedDeltaTime;
        walkForce.z = moveDelta.z * mass * walkSpeed * Time.fixedDeltaTime;
        state = walkForce.sqrMagnitude > 0 ? CharacterState.Walk : CharacterState.Idle;

        if (state == CharacterState.Idle) {
            animationController.Idle();
        }
        else {
            float direction = walkForce.z > 0 ? 1 : -1;
            animationController.Walk(direction);
        }
        rigidBody.AddRelativeForce(walkForce, ForceMode.VelocityChange);
    }

    protected override void Run() {
        Vector3 runForce = Vector3.zero;
        runForce.x = moveDelta.x * mass * runSpeed * Time.fixedDeltaTime;
        runForce.z = moveDelta.z * mass * runSpeed * Time.fixedDeltaTime;
        float runForceSqrMagnitude = runForce.sqrMagnitude;
        state = runForceSqrMagnitude > 0 ? CharacterState.Run : CharacterState.Idle;
        if (state == CharacterState.Idle) {
            animationController.Idle();
        }
        else {
            float direction = runForce.z > 0 ? 1 : -1;
            animationController.Run(direction);
        }
        rigidBody.AddRelativeForce(runForce, ForceMode.VelocityChange);
    }

    protected override void Look() {
        Quaternion newRotation = Quaternion.Euler(lookDelta.x, 0, 0);
        eyesTransform.localRotation = Quaternion.Slerp(eyesTransform.localRotation, newRotation, lerpSpeed);
        newRotation = Quaternion.Euler(0, lookDelta.y, 0);
        rigidBody.MoveRotation(newRotation);
    }

    public override void Die() {
        eyesTransform.SetParent(head);
        base.Die();
    }
}