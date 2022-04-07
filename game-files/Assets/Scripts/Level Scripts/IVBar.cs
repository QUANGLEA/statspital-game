using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public HealthBar healthbar;
    public int maxValue;
    public float currentValue;
    public int reduceSpeed = 2;

    private float timer;
    private int delayAmount = 1; // Second count

    // Start is called before the first frame update
    void Start()
    {
        currentValue = 0;
        maxValue = 8;
        SetMaxValue(maxValue, currentValue);
        //InvokeRepeating("IVCooldown", 1.0f, 1.0f);
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //IVCooldown();
    }

    void IVCooldown()
    {
        while (currentValue > 0)
        {
            timer += Time.deltaTime;

            if (timer >= delayAmount)
            {
                timer = 0f;
                currentValue--;
                SetValue(currentValue);
            }
        }
    }

    private IEnumerator SpawnTimer()
    {

        while (true)
        {
            yield return new WaitForSeconds(1);
            if (currentValue > 0)
            {
                SetValue(-1);
            }  
        }
    }

    public void SetMaxValue(int maxValue, float currentValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void SetValue(float value)
    {
        currentValue += value;
        slider.value = currentValue;
    }
}
