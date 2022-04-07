using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMedicine : MonoBehaviour
{
    public Sprite pillSprite;
    public PlayerController playerController;
    public PillType pillType;
    public string playerIsOn;

    // Update is called once per frame
    void Update()
    {
        PickUpMedicine();
    }

    // Check if the player pressed Space when in contact with a medicine cupboard 
    void PickUpMedicine()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsOn == gameObject.name)
        {
            playerController.pillIsOn = true;
            playerController.pillOnHead.sprite = pillSprite;
            playerController.currentPill = pillType;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOn = gameObject.name;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsOn = "";
        }
    }
}
