using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{


    private Rigidbody2D rb;
    private Animator anim;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJump = 2;
    private int jumpCount;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity")]
    [SerializeField] private float baseGravity = 2f;
    [SerializeField] private float maxFallSpeed = 18f;
    [SerializeField] private float fallSpeedMultiplier = 2f;


    private bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        IsGrounded();
        Gravity();
        Flip();

        anim.SetBool("walk",horizontalMovement != 0);
        anim.SetBool("grounded", Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer));
        
    }

    public void Move(InputAction.CallbackContext context){
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context){

        if (jumpCount <= 0)
            return;

        if (context.performed){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;
            anim.SetTrigger("jump");
        }

        else if (context.canceled){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpCount --;
            anim.SetTrigger("jump");
        }
    }

    private void Gravity(){
        if (rb.linearVelocity.y < 0){
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));

        }

        else
            rb.gravityScale = baseGravity;
    }

    private void IsGrounded()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer)){
            jumpCount = maxJump;
        }
    }

    private void Flip(){
        if (horizontalMovement > 0.01f && !isFacingRight){
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = true;
            
        }

        else if (horizontalMovement < -0.01f && isFacingRight){
            transform.Rotate(0f, -180f, 0f);
            isFacingRight = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }


    public bool canAttack()
    {
        return horizontalMovement == 0 && Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
    }

}
