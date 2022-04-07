using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] int numLevels;

    // Select Level Buttons 
    [SerializeField] Button tutorialLevelButton;
    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button level3Button;

    // Level Description Screen
    [SerializeField] Button backButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Image levelThumbnail;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject selectLevelScreen;

    // Star Requirements
    [SerializeField] GameObject starsScreen;
    [SerializeField] TextMeshProUGUI star1Text;
    [SerializeField] TextMeshProUGUI star2Text;
    [SerializeField] TextMeshProUGUI star3Text;

    // Level Information
    public Sprite[] levelThumbnails;
    private string [] levelDescriptions;
    private int[][] starsDescriptions;

    // Start is called before the first frame update
    void Start()
    {
        AddLevelDescriptions();
        AddStarsDescriptions();
        backButton.onClick.AddListener(() => SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU));
        tutorialLevelButton.onClick.AddListener(() => OpenLevel(0));
        level1Button.onClick.AddListener(() => OpenLevel(1));
        level2Button.onClick.AddListener(() => OpenLevel(2));
        level3Button.onClick.AddListener(() => OpenLevel(3));
    }

    // This function adds level description to the levelDescriptions array that will be displayed in the level selection scene 
    void AddLevelDescriptions()
    {
        levelDescriptions = new string[numLevels];
        levelDescriptions[0] = "The tutorial will help you learn about basic controls and how to help the patients of Statspital."; // Tutorial
        levelDescriptions[1] = "Objective: Save at least " + LevelInfo.level1PatientsForStars[0] + " patients to pass the level";
        levelDescriptions[2] = "Objective: Save at least " + LevelInfo.level2PatientsForStars[0] + " patients to pass the level";
        levelDescriptions[3] = "Objective: Save at least " + LevelInfo.level3PatientsForStars[0] + " patients to pass the level";
    }

    void AddStarsDescriptions()
    {
        starsDescriptions = new int[numLevels][];
        starsDescriptions[0] = LevelInfo.tutorialPatientsForStars; // Tutorial
        starsDescriptions[1] = LevelInfo.level1PatientsForStars;
        starsDescriptions[2] = LevelInfo.level2PatientsForStars;
        starsDescriptions[3] = LevelInfo.level3PatientsForStars;
    }

    void OpenLevel(int numLevel)
    {
        // Check if level is tutorial
        if (numLevel == 0) 
        {
            starsScreen.SetActive(false);
        }
        else
        {
            starsScreen.SetActive(true);
        }

        selectLevelScreen.SetActive(true);
        descriptionText.text = levelDescriptions[numLevel];
        levelThumbnail.sprite = levelThumbnails[numLevel];
        star1Text.text = starsDescriptions[numLevel][0].ToString();
        star2Text.text = starsDescriptions[numLevel][1].ToString();
        star3Text.text = starsDescriptions[numLevel][2].ToString();
        startGameButton.onClick.AddListener(() => SceneManager.LoadScene((int)SceneIndexes.TUTORIAL + numLevel));
    }


}
