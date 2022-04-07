using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // General Level Info
    [SerializeField] int numPatients;
    [SerializeField] int levelNum;
    [SerializeField] SubmitData submitData;

    // Health Bars
    public HealthBar[] healthBars;

    // Set Pill Effectiveness 
    public int yMedHealth;
    public int bMedHealth;
    public int comMedHealth;

    //Timer
    public Timer timer;

    // Game Winning Conditions 
    public bool gameOver;
    public bool isWinning;
    public int sufficientHealth;
    public int startingHealth;

    // Game Over Screen
    public GameObject gameOverScreen;
    public Button gameOverRestartButton;
    public Button gameOverMainMenuButton;

    // Level Completed Screen
    [SerializeField] GameObject levelCompleteScreen;
    [SerializeField] Button levelCompleteRestartButton;
    [SerializeField] Button levelCompleteMainMenu;
    [SerializeField] TextMeshProUGUI timeText;

    // Stars system
    [SerializeField] int stars = 0;
    [SerializeField] int patientsForOneStar;
    [SerializeField] int patientsForTwoStar;
    [SerializeField] int patientsForThreeStar;

    // Star images 
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    public Sprite filledStar;

    // Statistics Background
    [SerializeField] TextMeshProUGUI numYMedText;
    [SerializeField] TextMeshProUGUI numBMedText;
    [SerializeField] TextMeshProUGUI numComMedText;
    [SerializeField] TextMeshProUGUI numPatientsCuredText;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateLevelInfo();
        gameOver = false;
        isWinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMedStats();
        CheckGameOver();
    }

    // Check game ending conditions
    void CheckGameOver()
    {
        CheckStars();

        // The player wins automatically when they rescue all patients, or rescue at least __ patients before the timer runs out
        if (isWinning || (gameOver && stars > 0))
        {
            Global.win = true;
            submitData.UploadAfterLevel();
            WinLevel();
            enabled = false;
            return;
        }

        // The player loses when the time runs out 
        if (gameOver)
        {
            Global.win = false;
            submitData.UploadAfterLevel();
            GameOver();
            enabled = false;
        }
    }

    // Checks if there are enough patients cured before ending the level 
    void CheckStars()
    {
        int patientsCured = 0;
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (healthBars[i].currentHealth >= sufficientHealth)
            {
                patientsCured++;

                if (patientsCured == patientsForOneStar)
                {
                    // Add one star
                    stars = 1;
                }
                else if (patientsCured == patientsForTwoStar)
                {
                    // Add two star
                    stars = 2;
                }
                else if (patientsCured == patientsForThreeStar)
                {
                    // Add three stars 
                    stars = 3;
                    isWinning = true;
                    gameOver = true;
                }
                Global.numHealthy = patientsCured;
                numPatientsCuredText.text = "Patients cured: " + Global.numHealthy + "/" + numPatients;
            }
        }
    }

    // Display the winning screen and stars received 
    void WinLevel()
    {
        if (stars >= 1)
        {
            star1.sprite = filledStar;
            if (stars >= 2)
            {
                star2.sprite = filledStar;
                if (stars >= 3)
                {
                    star3.sprite = filledStar;
                }
            }
        }
        Global.time = timer.totalTime - timer.timeRemaining;
        timeText.text = "Time: " + Global.time.ToString("F2");
        levelCompleteScreen.SetActive(true);
        levelCompleteMainMenu.onClick.AddListener(() => SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU));
        levelCompleteRestartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    // This function updates the number of medicine given to the patients on the statistics board 
    void UpdateMedStats()
    {
        int totalYMed = Global.yMed.Sum();
        int totalBMed = Global.bMed.Sum();
        int totalComMed = Global.comMed.Sum();
        float yMedEffectiveness = (totalYMed * yMedHealth * 100) / (numPatients * 100 - startingHealth);
        float bMedEffectiveness = (totalBMed * bMedHealth * 100) / (numPatients * 100 - startingHealth);
        float comMedEffectiveness = (totalComMed * comMedHealth * 100) / (numPatients * 100 - startingHealth);
        numPatientsCuredText.text = "Patients cured: " + Global.numHealthy + "/" + numPatients;
        numYMedText.text = yMedEffectiveness+ "%";
        numBMedText.text = bMedEffectiveness + "%";
        numComMedText.text = comMedEffectiveness + "%";
    }

    // Initialize general game information to global variables that will be sent to database 
    void UpdateLevelInfo()  
    {
        Global.numHealthy = 0;
        Global.level = levelNum;
        Global.numPatients = numPatients;
        Global.patient = new int[numPatients];
        Global.yMed = new int[numPatients];
        Global.bMed = new int[numPatients];
        Global.comMed = new int[numPatients];
        Global.health = new int[numPatients];
        for (int i = 0; i < numPatients; i++)
        {
            Global.patient[i] = i + 1;
        }
    }

    // Display the game over screen 
    void GameOver()
    {
        Global.time = timer.totalTime - timer.timeRemaining;
        gameOverScreen.SetActive(true);
        gameOverMainMenuButton.onClick.AddListener(() => SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU));
        gameOverRestartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
}
