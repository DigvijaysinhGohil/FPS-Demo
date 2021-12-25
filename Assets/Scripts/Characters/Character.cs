using UnityEngine;

public class Character : MonoBehaviour {
    private const int MAX_HEALTH = 100;

    protected int health;

    [SerializeField] protected float walkSpeed = 2;
    [SerializeField] public float runSpeed = 5;

    [Header("Mass in Kilograms"), SerializeField]
    protected float mass = 50;

    public CharacterState state;
    
    [Space, SerializeField] protected CharacterAnimationController animationController;

    protected void FixedUpdate() {
        Look();
    }

    protected virtual void Look() { }

    protected virtual void Walk() { }

    protected virtual void Run() { }
}