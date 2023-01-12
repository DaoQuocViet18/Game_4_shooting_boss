using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject door;
    Shooting shooting;
    HealthMana healthMana;
    boomerang boomerang;
    SpawnManage SP;

    private void Start()
    {
        shooting = GameObject.Find("Weapon").GetComponent<Shooting>();
        healthMana = GameObject.Find("Player").GetComponent<HealthMana>();
        SP = GameObject.Find("SpawnManage").GetComponent<SpawnManage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SP.active_Spawn_begin();
            door.SetActive(true);
            if (shooting.IsReady[1] == false && healthMana.sliderMana.value == healthMana.sliderMana.maxValue)
            {
                boomerang = GameObject.Find("boomerang_1(Clone)").GetComponent<boomerang>();

                shooting.ResetBoomerang();
                Destroy(boomerang.gameObject);

            }

            Destroy(gameObject); 
        }
    }
}
