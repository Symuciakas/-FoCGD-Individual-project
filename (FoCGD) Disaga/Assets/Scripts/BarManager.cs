using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textDisplay;

    public void SetSliderMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void SetSliderValue(int value)
    {
        slider.value = value;
        textDisplay.text = slider.value + "/" + slider.maxValue;
    }
}