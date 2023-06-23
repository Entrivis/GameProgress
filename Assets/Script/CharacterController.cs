using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /*
        Movement
    */
    [Header("Movement")]
    private float _input;
    public float absInput;
    public Animator animator;
    private Vector3 speed = new Vector3(5, 0, 0);

    /*
        Jump
    */
    
    [Header("JumpPhysics")]
    public bool isGrounded;
    public LayerMask groundLayer;
    public float groundLength;
    private Rigidbody2D rb;
    public float jumpForce = 15f;
    public bool isJumping;
    private Vector2 gravity;
    public float jumpMultiplier;
    public float fallMultiplier;

    [Header("JumpTime")]
    public float jumpTime;
    private float jumpTimeCounter;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = new Vector2(0, -Physics2D.gravity.y);
    }
    private void Update()
    {
        Movement();
        Flip();
        Jump();
    }
    private void Movement()
    {
        /*
            Movement
        */
        _input = Input.GetAxisRaw("Horizontal");
        transform.position += _input * speed * Time.deltaTime;
        absInput = Mathf.Abs(_input);
        
    }
    private void Flip()
    {
        /*
            FlipSprite
        */
        if (_input > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (_input < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Jump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);
        if(isGrounded) jumpTimeCounter = 0;
        if(isGrounded && Input.GetButtonDown("Jump")){
            // print("Jump");
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if(isJumping){
            jumpTimeCounter += Time.deltaTime;
            print(jumpTimeCounter);
            if(jumpTimeCounter > jumpTime){
                print("JumpTimeCounter");
                isJumping = false;
            }
            rb.velocity += jumpMultiplier * gravity * Time.deltaTime;
        }
        if(Input.GetButtonUp("Jump")){
            isJumping = false;
            jumpTimeCounter = 0;
            if(rb.velocity.y > 0) rb.velocity =  new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); 
        }
        // print(isJumping);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }
}
