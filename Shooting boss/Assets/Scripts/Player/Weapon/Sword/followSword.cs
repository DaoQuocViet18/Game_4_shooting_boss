using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSword : MonoBehaviour
{
    [SerializeField] Transform sword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sword.position;
        transform.rotation = sword.rotation;
    }
}
