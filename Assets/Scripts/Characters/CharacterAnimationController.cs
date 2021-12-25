using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour {
    private const string SPEED = "Speed";
    private const string DEATH = "Death";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Idle() {
        animator.SetFloat(SPEED, 0);
    }

    public void Walk(float direction) {
        animator.SetFloat(SPEED, 1 * direction);
    }

    public void Run(float direction) {
        animator.SetFloat(SPEED, 2 * direction);
    }

    public void Death() {
        animator.SetTrigger(DEATH);
    }
}