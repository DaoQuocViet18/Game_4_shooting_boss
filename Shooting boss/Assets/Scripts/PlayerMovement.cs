using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float speed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirtection;
    public  Rigidbody rb;

    [Header("acceleration")]
    public float highSpeed;
    public KeyCode hightKey = KeyCode.RightAlt;
    private float limitSpeed; 
    private bool highted;
    private Animator anim;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    public KeyCode jumpKey = KeyCode.Space;
    private bool readyToJump;


    [Header("Ground Check")]
    public float playerHeight;
    public float groundDrag;
    public LayerMask whatIsGround;
    private bool grounded;



    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Weapon").GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        SpeedControl();
        GroundDrag();
    }

    private void FixedUpdate()
    {
        MyMove();
        anim.SetFloat("Speed", speed);

        if (speed >= 25)
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);

    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            MyJump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if (Input.GetKey(hightKey) && grounded)
        {
            highted = true;
            limitSpeed = moveSpeed * highSpeed;
        }    
        else
        {
            highted = false;
            limitSpeed = moveSpeed;
        }

    }

    void MyMove()
    {
        // calculate movement direction
        moveDirtection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (highted)
            speed = moveSpeed * highSpeed;
        else if (grounded)
            speed = moveSpeed;
        else if (!grounded)
            speed = moveSpeed * airMultiplier;

        rb.AddForce(moveDirtection * speed * 10f, ForceMode.Force);
    }

    void GroundDrag()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > limitSpeed)
        {
            Vector3 limtedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limtedVel.x, rb.velocity.y, limtedVel.z);
        }
    }

    void MyJump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
