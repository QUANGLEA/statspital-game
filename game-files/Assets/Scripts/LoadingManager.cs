using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI loadingText;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(GameManager.sceneToLoad);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        float progress = 0;
        loadingScreen.SetActive(true);
        while (progress < 1)
        {
            float mult = 0.1f;
            progress = Mathf.Clamp01(progress + mult);
            mult *= 1.5f;
            progressBar.value = progress;
            loadingText.text = "Loading: " + (int)(progress * 100f) + "%";
            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
