using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirtection;
    Rigidbody rb;

    [Header("acceleration")]
    public float highSpeed;
    public KeyCode hightKey = KeyCode.RightAlt;
    private float limitSpeed; 
    private bool highted;

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
            rb.AddForce(moveDirtection * moveSpeed * 10f * highSpeed, ForceMode.Force);
        else if (grounded)
            rb.AddForce(moveDirtection * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirtection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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
