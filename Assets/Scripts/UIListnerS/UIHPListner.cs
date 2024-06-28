using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPListner : MonoBehaviour
{
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        Player.HPUpdate += SliderUpdate;
    }

    private void SliderUpdate(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
