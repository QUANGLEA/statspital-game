using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int sceneToLoad;

    public static void StartLoading(int sceneNum)
    {
        sceneToLoad = sceneNum;
        // SceneManager.LoadScene((int) SceneIndexes.LOADING);
    }
}
