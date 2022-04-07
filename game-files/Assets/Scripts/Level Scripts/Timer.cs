using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Game Manager
    public LevelManager levelManager;

    // Timer 
    public float totalTime;
    [HideInInspector] public float timeRemaining;
    public TextMeshProUGUI timeText;

    [SerializeField] Slider timerBar;
    [SerializeField] Image timerFill;
    [SerializeField] Gradient timerGradient;
    [SerializeField] Image hourglass;

    [SerializeField] float speed = 2f;
    [SerializeField] float maxRotation = 10f;
    [SerializeField] float criticalTime;

    void Start()
    {
        // Starts the timer automatically
        timeRemaining = totalTime;  
        SetMaxTime(timeRemaining);
    }
    void Update()
    {
        if (!levelManager.gameOver)
        {
            if (timeRemaining < criticalTime)
            {
                hourglass.transform.rotation = Quaternion.Euler(0f, 0f, -20 + maxRotation * Mathf.Sin(Time.time * speed));
            }

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                SetTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                SetTime(timeRemaining);
                levelManager.gameOver = true;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetMaxTime(float time)
    {
        timerBar.maxValue = time;
        timerBar.value = time;

        timerFill.color = timerGradient.Evaluate(1f);
    }

    public void SetTime(float time)
    {
        timerBar.value = time;
        timerFill.color = timerGradient.Evaluate(timerBar.normalizedValue);
    }
}