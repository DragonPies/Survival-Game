using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class movement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Rigidbody rb;


    [Header("Components Needed")]
    [SerializeField] private Transform Player;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Transform Camera;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float currentSpeed;
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;
    [Space]
    [Header("Jumping")]
    [SerializeField] private float JumpForce;
    [SerializeField] private float jumpCount;
    [SerializeField] private bool isGrounded;
    [Space]
    [Header("Running")]
    [SerializeField] private bool Run = false;
    [SerializeField] private float RunSpeed;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = Speed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
            isGrounded = true;
            jumpCount = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void Move()
    {
        Vector3 movement = Camera.forward * moveAction.action.ReadValue<Vector2>().y + Camera.right * moveAction.action.ReadValue<Vector2>().x;
        movement.y = 0f; // Ensure movement is only on the horizontal plane
        PlayerMovementInput = movement;
        rb.MovePosition(transform.position + PlayerMovementInput * (Time.fixedDeltaTime * currentSpeed));
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

    public void Runmode()
    {
        if (!Run)
        {
            currentSpeed = RunSpeed;
            Run = true;
        }
        else if (Run)
        {
            currentSpeed = Speed;
            Run = false;
        }
    }



    public void Jumping()
    {
        if (isGrounded && !Sneak)
        {
            jump();
        }

        else if (!isGrounded && !Sneak && jumpCount < 1)
        {
            jump();
        }
    }

    private void jump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        jumpCount++;
    }
}