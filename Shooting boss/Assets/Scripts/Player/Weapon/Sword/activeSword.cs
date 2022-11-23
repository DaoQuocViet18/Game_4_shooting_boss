using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeSword : MonoBehaviour
{
    public GameObject sword_collider;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sword.activeInHierarchy == false)
        {
            sword_collider.gameObject.SetActive(false);
        }
        else if (sword.activeInHierarchy == true)
        {
            sword_collider.gameObject.SetActive(true);
        }
    }
}
