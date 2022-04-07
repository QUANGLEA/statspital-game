using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Button exitTutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        exitTutorialButton.onClick.AddListener(ExitTutorial);
    }

    void ExitTutorial()
    {
        SceneManager.LoadScene((int)SceneIndexes.MAIN_MENU);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
