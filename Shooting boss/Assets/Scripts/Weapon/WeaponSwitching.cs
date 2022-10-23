using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {    
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectWeapon;

        // The first way
        if (Input.GetAxis("Mouse ScrollWheel")>0)
        {
            if (selectWeapon >= transform.childCount - 1)
                selectWeapon = 0;
            else
                selectWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectWeapon <= 0)
                selectWeapon = transform.childCount - 1;
            else
                selectWeapon--;
        }

        // the second way
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectWeapon = 1;
        }

        if (previousSelectedWeapon != selectWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform) 
        {
            if (i == selectWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
