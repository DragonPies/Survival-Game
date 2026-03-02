using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class movement : MonoBehaviour
{
    public Vector3 PlayerMovementInput;
    private Rigidbody rb;
    //public Vector3 direction;



    [Header("Components Needed")]
    [SerializeField] private Transform Player;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Transform Camera;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    private float currentSpeed;
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;
    [Space]
    [Header("Jumping")]
    //[SerializeField] private float jumpSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float jumpCount;
    [SerializeField] private bool isGrounded;
    [Space]
    [Header("Running")]
    [SerializeField] private bool Run = false;
    [SerializeField] private float RunSpeed;
    [Space]
    [Header("Sliding")]
    public bool sliding;
    public float maxSlideTime;
    private float slideTimer;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = Speed;
        slideTimer = maxSlideTime;
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
        Slide();
    }

    public void Sneakmode(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;
        Debug.Log("Sneak Toggled");
        if (!Sneak)
        {
            currentSpeed = SneakSpeed;
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            Sneak = true;
        }
        else if (Sneak)
        {   
            currentSpeed = Speed;
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneak = false;
        }
    }

    public void Runmode(InputAction.CallbackContext ctx)
    {

           if (!ctx.performed)
                return;
        if (!Run)
        {
            currentSpeed = RunSpeed;
            Run = true;
        }
        else
        {
            currentSpeed = Speed;
            Run = false;
        }

    }



    public void Jumping(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed)
            return;
        if (isGrounded && !Sneak)
        {
            jump();
            Debug.Log("Jumped");
            //currentSpeed = jumpSpeed;
        }

        else if (isGrounded && Sneak)
        {
            currentSpeed = Speed;
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneak = false;
        }

        else if (!isGrounded && !Sneak && jumpCount < 2)
        {
            Debug.Log("Double Jumped");
            jump();
        }
    }

    private void jump()
    {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        jumpCount++;
    }


    private void Slide()
    {
        if (Run && Sneak)
        { 
            sliding = true;
            if (sliding && slideTimer > 0f)
            {
                Player.localScale = new Vector3(1f, 0.25f, 1f);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                currentSpeed = (RunSpeed * 1.5f);
                slideTimer -= Time.deltaTime;
            }
            else
            {
                sliding = false;
                currentSpeed = Speed;
                Player.localScale = new Vector3(1f, 1f, 1f);
                slideTimer = maxSlideTime;
                Run = false;
                Sneak = false;
            }

        }

    }


}