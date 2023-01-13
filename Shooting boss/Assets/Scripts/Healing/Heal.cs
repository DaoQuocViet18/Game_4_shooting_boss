using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private bool moveUp;

    private void Update()
    {
        transform.Rotate(Vector3.up * 3 * 10 * Time.deltaTime, Space.Self);

        if (transform.position.y < 3)
            moveUp = true;
        else if (transform.position.y >= 5)
            moveUp = false;

        if (moveUp == true)
        transform.Translate(Vector3.up * 6 * Time.deltaTime, Space.Self);
        else 
            transform.Translate(Vector3.down * 6 * Time.deltaTime, Space.Self);


    }
}
