using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixerLoad : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxTime(int mixTime)
    {
        slider.maxValue = mixTime;
        slider.value = mixTime;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime(int mixTime)
    {
        slider.value = mixTime;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
