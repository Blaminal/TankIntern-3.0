using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private float maxSpeed = 150;
    [SerializeField]
    private float rotationSpeed = 100;
    [SerializeField]
    private float acceleration = 70;
    [SerializeField]
    private float deacceleration = 50;
    [SerializeField]
    private float maximumHealth = 100;
    [SerializeField] // TODO: Have the bullet inform the player of how much health it should subtract
    private float bulletDamage = -10f;
    [SerializeField]
    private Slider slider;

    private Vector2 movementInput = Vector2.zero;

    private float currentHealth;
 
    public GameManager gameManager; // getters and setter look into
    
    //private float currentSpeed = 0;
    //private float currentForewardDirection = 1;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maximumHealth;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.right * movementInput.y * 100 * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * rotationSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            ChangeHp(bulletDamage);
        }
    }

    // once its full, don't show, when damaged show
    private void ChangeHp(float amount) {
        currentHealth += amount;
        slider.value = currentHealth;
        if (currentHealth == 0) {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer() {
        // Do something
        Destroy(transform.parent.gameObject);
    }

    public void ChangeScoreOfPlayer(int amountToChange) {
        gameManager.ChangeScoreOfPlayer(playerInput, amountToChange);
    }

}
