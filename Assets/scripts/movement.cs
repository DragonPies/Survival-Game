using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class movement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Rigidbody rb;
    private float distance = 5f;


    [Header("Components Needed")]
    [SerializeField] private Transform Player;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Transform Camera;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField]private float currentSpeed;
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;
    [Space]
    [Header("Jumping")]
    [SerializeField] private float JumpForce;
    [SerializeField] private bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = Speed;
    }

    private void FixedUpdate()
    {
        Move();
        transform.position = transform.position + Camera.transform.forward * distance * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("land"))
        {
            isGrounded = true;
        }
    }

    private void Move()
    {
        PlayerMovementInput = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        rb.MovePosition(transform.position + PlayerMovementInput * Time.fixedDeltaTime * currentSpeed);
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
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}