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
    //public float smooth = 8;
    //public float pull = 4;
    public KeyCode backKey = KeyCode.RightShift;

    [Header("teleport")]
    PlayerCam playerCam;
    Shooting shooting;
    public float time_Tel;
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
        transform.Rotate(Vector3.up * -rotaSpeed, Space.Self);

        if ((transform.position - originPos).magnitude >= limit) 
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }

        InputA();

    }

    void InputA()
    {
        if (Input.GetKeyDown(telKey))
        {
            playerCam.teleport.Play();
            Invoke("teleport_Effect", time_Tel);
        }

        if (Input.GetKeyDown(backKey))
        {
            shooting.ResetBoomerang();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
        
    }

    void teleport_Effect ()
    {
        playerCam.orientation.position = transform.position;
        shooting.ResetBoomerang();
        Destroy(gameObject);
    }

    //void BoomerangBack ()
    //{

    //    for (int i = 0; i < smooth; i++)
    //    {
    //        GetBQCpoint(i/(float)(smooth+1), )
    //    }
    //}

    //Vector3 GetBQCpoint (float t, Vector3 p0, Vector3 p1, Vector3 p2)
    //{
    //    float u = 1 - t;
    //    float uu = u * u;
    //    float tt = t * t;
    //    Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
    //    return p;
    //}
}
