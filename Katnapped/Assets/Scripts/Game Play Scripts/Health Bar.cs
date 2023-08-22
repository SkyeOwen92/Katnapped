using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public void SetMaxHealth( float hp)
    {
        healthBar.maxValue = hp;
        healthBar.value = hp;

    }

    // Update is called once per frame
    public void SetCurHealth(float hp)
    {
        healthBar.value = hp;
    }
}
