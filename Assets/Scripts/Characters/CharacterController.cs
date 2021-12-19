using UnityEngine;

public class CharacterController : MonoBehaviour {
    private const int MAX_HEALTH = 100;
    
    protected int health;
    
    [Header("Speeds In Meters/Second"),SerializeField] protected float walkSpeed = 2;
    [SerializeField] protected float runSpeed = 5;

    private void FixedUpdate() {
        Look();
    }

    protected virtual void Look() { }
}