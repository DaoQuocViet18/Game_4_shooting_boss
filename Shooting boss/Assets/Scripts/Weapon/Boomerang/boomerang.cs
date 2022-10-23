using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class boomerang : MonoBehaviour
{
    [Header("throw")]
    Rigidbody rb;
    public float rotaSpeed = 10;
    public float shootForce = 40;
    public float limit = 20;
    private Vector3 originPos;

    [Header("throw back")]
    public KeyCode backKey = KeyCode.RightShift;

    [Header("teleport")]
    PlayerCam playerCam;
    Shooting shooting;
    public KeyCode telKey = KeyCode.Mouse1;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        rb = GetComponent<Rigidbody>();
        playerCam = GameObject.Find("Main Camera").GetComponent<PlayerCam>();
        shooting = GameObject.Find("Weapon").GetComponent<Shooting>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - originPos).magnitude >= limit) 
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
            transform.Rotate(Vector3.up * -rotaSpeed, Space.Self);

        if (Input.GetKey(telKey))
        {
            playerCam.orientation.position = transform.position;
            shooting.ResetBoomerang();
            Destroy(gameObject);
        }

        if (Input.GetKey(backKey))
        {
            shooting.ResetBoomerang();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

}
