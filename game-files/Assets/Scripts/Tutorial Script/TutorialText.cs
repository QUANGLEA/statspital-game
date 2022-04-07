using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject continueButton;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

    } 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if(textDisplay.text == sentences[index]){
            continueButton.SetActive(true);
        }
    }

    public void NextSentence(){
        continueButton.SetActive(false);
        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
    // Update is called once per frame
   
}
