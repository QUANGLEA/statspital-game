using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMedicine : MonoBehaviour
{
    // Each patient on a level will be assigned a number (in the inspector from 1 to n)
    public int patientNum = 0;

    public LevelManager levelManager;
    public PlayerController playerController;
    public HealthBar healthBar; 
    public string playerIsOn;
    bool canGiveMed;

    // Set IV Bar
    public IVBar ivBar;
    public int ivTimer = 8;

    void Start()
    {
        Global.patient[patientNum - 1] = patientNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (ivBar.currentValue > 0)
        {
            canGiveMed = false;
        }
        else
        {
            canGiveMed = true;
        }

        GivePill();
    }

    // Give patient pill, start IV bar, and add health 
    void GivePill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsOn == gameObject.name && canGiveMed)
        {
            if (playerController.currentPill == PillType.YMed)
            {
                Global.yMed[patientNum - 1] += 1;
                AddHealth(levelManager.yMedHealth);
                playerController.pillIsOn = false;
            }
            else if (playerController.currentPill == PillType.BMed)
            {
                Global.bMed[patientNum - 1] += 1;
                AddHealth(levelManager.bMedHealth);
                playerController.pillIsOn = false;
            }
            else if (playerController.currentPill == PillType.ComMed)
            {
                Global.comMed[patientNum - 1] += 1;
                AddHealth(levelManager.comMedHealth);
                playerController.pillIsOn = false;
            }
        }
    }

    // Checks for collision with Player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOn = gameObject.name;   
        }
    }

    // Check for exit collision with Player
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOn = "";
        }
    }

    public void AddHealth(int health)
    {
        ivBar.SetValue(ivTimer);
        StartCoroutine(HealthCooldownCoroutine(health));
    }

    IEnumerator HealthCooldownCoroutine(int health)
    {
        yield return new WaitForSeconds(ivTimer);
        if (levelManager.gameOver) { yield break; } // Stop the patient from getting health increase if timer ends
        healthBar.currentHealth += health;
        Global.health[patientNum - 1] = healthBar.currentHealth;
        healthBar.SetHealth(healthBar.currentHealth);
    }

}
