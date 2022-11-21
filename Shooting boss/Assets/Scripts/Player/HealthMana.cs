using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMana : MonoBehaviour
{
    Shooting shooting;
    public Slider sliderMana;
    bool recovered = false;
    private void Start()
    {
        shooting = GameObject.Find("Weapon").GetComponent<Shooting>();
    }


    private void Update()
    {
        if (sliderMana.value < sliderMana.maxValue)
        {
            sliderMana.value = Mathf.MoveTowards(sliderMana.value, sliderMana.maxValue, Time.deltaTime * 0.5f);
            recovered = true;
        }    
        else if (shooting.IsReady[1] == false && recovered == true)
        {
            shooting.IsReady[1] = true;
            recovered = false;
        }    
    }

    public void setMaxMana(int mana)
    {
        sliderMana.maxValue = mana;
        sliderMana.value = mana;
    }

    public void takeMana()
    {
        sliderMana.value = 0;
    }

}
