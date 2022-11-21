using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public virtual void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
        fill.color = gradient.Evaluate(1f);
    }

    public virtual void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
