using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Movement and Rotation 
    public float speed;
    public float rotationSpeed;
    
    // Player Rigidbody
    private Rigidbody playerRb;

    // Pill On Head 
    public Image pillOnHead;
    public bool pillIsOn;
    public bool mixIsOn;

    public PillType currentPill = PillType.None;

    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    { 
        pillIsOn = false;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!levelManager.gameOver)
        {
            CheckPill();
            HandleMovement();
        }
    }

    // If the player picks up a pill, then enable the image of the pill 
    void CheckPill()
    {
        if (pillIsOn)
        {
            pillOnHead.enabled = true;
        }
        else
        {
            pillOnHead.enabled = false;
            currentPill = PillType.None;
        }
    }

    // This function controls the movement and rotation of the player character
    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;
        playerRb.MovePosition(transform.position + movementInput * Time.deltaTime * speed);
        if (movementInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
