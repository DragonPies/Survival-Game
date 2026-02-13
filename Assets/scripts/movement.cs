using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private bool Sneaking = false;
    private Rigidbody rb;


    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform Player;
    [SerializeField] private InputActionReference moveAction;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    private float currentSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Gravity = 9.81f;
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;

    private bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = Speed;
        Velocity.y = -1f;
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        rb.MovePosition(transform.position + PlayerMovementInput * Time.fixedDeltaTime * currentSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("land"))
        {
            isGrounded = true;
        }
    }

    public void Sneakmode()
    {
        if (!Sneak)
        {   
            currentSpeed = SneakSpeed;
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            Sneak = true;
        }
        else if (Sneak)
        {   
            currentSpeed = Speed;
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneak = false;
        }
    }

 

    public void Jumping()
    { 
        if (isGrounded && !Sneak)
        {
            Velocity.y = JumpForce;
        }
        else
        {
            Velocity.y += Gravity * -2f * Time.deltaTime;
        }
    
    }
}