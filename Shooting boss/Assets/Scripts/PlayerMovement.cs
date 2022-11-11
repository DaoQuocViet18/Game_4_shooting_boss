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

    [Header("Running")]
    public float highSpeed;
    public KeyCode hightKey = KeyCode.RightAlt;
    private float limitSpeed; 
    public  bool running;
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

    [Header("Wall Check")]
    public int rayAmount;
    public float halfRange;
    public float distance;

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
        BoundWall(rayAmount, halfRange);
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
            running = true;
            limitSpeed = moveSpeed * highSpeed;
        }    
        else
        {
            running = false;
            limitSpeed = moveSpeed;
        }

    }

    void MyMove()
    {
        // calculate movement direction
        if (running)
            moveDirtection = transform.forward * verticalInput;
        else
            moveDirtection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (running)
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

    void BoundWall(int rayAmount, float halfRange)
    {
        Vector3[] dirs = new Vector3[rayAmount];
        float angularStep = 2f * Mathf.PI / (float)rayAmount;
        float currentAngle = angularStep / 2f;

        for (int i = 0; i < rayAmount; ++i)
        {
            dirs[i] = transform.right * Mathf.Cos(currentAngle) + transform.forward * Mathf.Sin(currentAngle);
            currentAngle += angularStep;
        }

        foreach (Vector3 dir in dirs)
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position + dir * halfRange , dir);
            Debug.DrawRay(ray.origin, ray.direction.normalized * distance, Color.red);
            if (Physics.Raycast(ray.origin, ray.direction.normalized * distance, out hit, distance))
            {
                Debug.DrawRay(hit.point, hit.normal * speed, Color.green);
                rb.AddForce(hit.normal.normalized * speed, ForceMode.Force);
            }
        }

        //for (int i = 0; i < 10; i++)
        //{
            
        //    Debug.DrawRay(transform.position, transform.forward * bound, Color.red);
        //    if (Physics.Raycast(transform.position, transform.forward, bound))
        //    {
        //        rb.AddForce(-moveDirtection * speed * 5f, ForceMode.Force);
        //    }
        //}
    }

}
