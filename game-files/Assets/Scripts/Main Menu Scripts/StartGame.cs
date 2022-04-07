using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class StartGame : MonoBehaviour
{
    public GameManager gameManager;

    // Welcome Screen
    public GameObject welcomeScreen;
    public Button playButton;
    public Button instructionsButton;
    public Button settingsButton;

    // Instructions Screen
    public GameObject instructionsScreen;
    public Button instructionsExitButton;

    // Settings
    public GameObject settingsScreen;
    public Button settingsExitButton;

    // Element Flow Controls
    EventSystem system;
    public Selectable firstInput;

    // Input System
    public TMP_InputField playerID;
    public TMP_InputField groupID;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
        playButton.interactable = false;
        playerID.text = PlayerPrefs.GetString("playerID");
        groupID.text = PlayerPrefs.GetString("groupID");

        playButton.onClick.AddListener(StartLevelSelection);
        instructionsButton.onClick.AddListener(OpenInstructions);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    void Update()
    {
        CheckIDInput();
        NavigateTab();
    }

    // This function checks whether the player has inputed a player and group ID before allowing them to press `Play` 
    void CheckIDInput()
    {
        if (!(string.IsNullOrEmpty(playerID.text) || string.IsNullOrEmpty(groupID.text)))
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }

    // This function allows the user to navigate between UI elements using TAB and LEFTSHIFT + TAB
    void NavigateTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
    }

    // This function starts the level selection phase, when `Start Game` button is pressed
    void StartLevelSelection()
    {
        Global.playerID = playerID.text;
        Global.groupID = groupID.text;
        PlayerPrefs.SetString("playerID", Global.playerID);
        PlayerPrefs.SetString("groupID", Global.groupID);
        SceneManager.LoadScene((int)SceneIndexes.LEVEL_SELECTION);
    }

    // This function opens the instructions, when the instructions button is pressed
    void OpenInstructions()
    {
        welcomeScreen.SetActive(false);
        instructionsScreen.SetActive(true);
        instructionsExitButton.onClick.AddListener(ExitInstructions);
    }

    // This function exits the instructions screen, when the exit button is pressed 
    void ExitInstructions()
    {
        instructionsScreen.SetActive(false);
        welcomeScreen.SetActive(true);
    }

    // This function opens the settings screen 
    void OpenSettings()
    {
        welcomeScreen.SetActive(false);
        settingsScreen.SetActive(true);
        settingsExitButton.onClick.AddListener(CloseSettings);
    }

    // This function closes the settings screen 
    void CloseSettings()
    {
        settingsScreen.SetActive(false);
        welcomeScreen.SetActive(true);
    }
}
