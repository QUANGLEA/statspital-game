using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixingTable : MonoBehaviour
{
    [SerializeField] Image yMed;
    [SerializeField] Image bMed;
    [SerializeField] Sprite comMed;
    [SerializeField] PlayerController playerController;
    [SerializeField] PillType pillType;

    // Start is called before the first frame update
    void Start()
    {
        yMed.enabled = bMed.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MixMedicine();
    }

    void MixMedicine()
    {
        if (playerController.mixIsOn)
        {
            if (Input.GetKeyDown(KeyCode.Return) && yMed.enabled && bMed.enabled && !playerController.pillIsOn)
            {
                yMed.enabled = bMed.enabled = false;
                playerController.pillOnHead.sprite = comMed;
                playerController.pillIsOn = true;
                playerController.currentPill = pillType;
            }

            if (Input.GetKeyDown(KeyCode.Space) && yMed.enabled == false && playerController.pillIsOn && playerController.currentPill == PillType.YMed)
            {
                yMed.sprite = playerController.pillOnHead.sprite;
                yMed.enabled = true;
                playerController.pillIsOn = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && bMed.enabled == false && playerController.pillIsOn && playerController.currentPill == PillType.BMed)
            {
                bMed.sprite = playerController.pillOnHead.sprite;
                bMed.enabled = true;
                playerController.pillIsOn = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.mixIsOn = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.mixIsOn = false;
        }
    }
}
