using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedLightGreenLightBotController : Character {
    private Rigidbody rigidBody;

    [SerializeField] private float minReactionTime = .3f;
    [SerializeField] private float maxReactionTime = .6f;

    [SerializeField] private LightsController lightsController;

    [Space] public SafetyState safetyState;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.mass = mass;
        safetyState = SafetyState.Unsafe;

        lightsController.OnRedLightStarted.AddListener(HandleLightChange);
        lightsController.OnGreenLightStarted.AddListener(HandleLightChange);
    }

    private void OnDestroy() {
        lightsController.OnRedLightStarted.RemoveListener(HandleLightChange);
        lightsController.OnGreenLightStarted.RemoveListener(HandleLightChange);
    }

    protected void FixedUpdate() {
        if (state != CharacterState.Dead) {
            base.FixedUpdate();
            Run();
        }
    }

    private void HandleLightChange() {
        if (state == CharacterState.Dead)
            return;

        StartCoroutine(CrHandleLightChange());
    }

    private IEnumerator CrHandleLightChange() {
        yield return new WaitForSeconds(Random.Range(minReactionTime, maxReactionTime));

        if (safetyState == SafetyState.Unsafe) {
            state = lightsController.IsGreenLightOn ? CharacterState.Run : CharacterState.Idle;
        }
    }

    protected override void Run() {
        Vector3 runForce = Vector3.zero;

        if (state == CharacterState.Idle) {
            animationController.Idle();
        }
        else if (state == CharacterState.Run) {
            runForce.z = mass * runSpeed * Time.fixedDeltaTime;
            float direction = 1;
            animationController.Run(direction);
        }

        rigidBody.AddRelativeForce(runForce, ForceMode.VelocityChange);
    }

    public override void Die() {
        base.Die();
        safetyState = SafetyState.Safe;
    }
}