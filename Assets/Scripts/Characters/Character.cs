using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {
    private const int MAX_HEALTH = 100;

    protected int health;

    [SerializeField] protected float walkSpeed = 2;
    [SerializeField] public float runSpeed = 5;

    [Header("Mass in Kilograms"), SerializeField]
    protected float mass = 50;

    public CharacterState state;

    [Space, SerializeField] protected CharacterAnimationController animationController;
    [SerializeField] protected CharacterAudioController audioController;

    [Space] public UnityEvent OnDeath;

    protected virtual void FixedUpdate() {
        Look();
    }

    protected virtual void Look() { }

    protected virtual void Walk() { }

    protected virtual void Run() { }

    public virtual void Die() {
        state = CharacterState.Dead;
        animationController.Death();
        audioController.RunSfx(false);
        audioController.DeathSfx();
        OnDeath?.Invoke();
    }
}