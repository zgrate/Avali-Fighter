using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 velocityPlayer;
    public float playerSpeed = 2.0f;
    private readonly float jumpHeight = 1.0f;
    private readonly float gravityValue = -9.81f;
    private bool grounded;

    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;
    private float xRot = 0.0f;
    private float yRot = 0.0f;

    private int kills = 0, hp = 0;
    public int maxHP = 3;

    public TextMeshProUGUI killsLabel, hpLabel;
    public GameObject gameOver;


    public bool paused = false;

    public UnityEvent gameObjectAction, gameRestartAction;

    private void Start()
    {
        hp = maxHP;
        hpLabel.text = "HP: " + hp;
        kills = 0;
        killsLabel.text = "Kills: " + 0;
    }

    internal void AddKill()
    {
        kills++;
        killsLabel.text = "Kills: " + kills;
    }

    internal void Damage()
    {
        hp -= 1;
        hpLabel.text = "HP: " + hp;
        if(hp == 0)
        {
            gameObjectAction.Invoke();
            gameOver.SetActive(true);
            paused = true;
        }
    }

    public void ResetGame()
    {

        hp = maxHP;
        hpLabel.text = "HP: " + hp;
        kills = 0;
        killsLabel.text = "Kills: " + 0;
        gameOver.SetActive(false);

        paused = false;
        gameRestartAction.Invoke();
    }

    void Update()
    {
        if(paused && Input.GetButtonDown("Jump")){
            ResetGame();
        }
        if (!paused)
        {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

            yRot += mouseX;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90, 90);

            gameObject.transform.eulerAngles = new Vector3(0, yRot, 0.0f);
            Camera.main.transform.localEulerAngles = new Vector3(xRot, 0, 0.0f);
            grounded = controller.isGrounded;
            if (grounded && velocityPlayer.y < 0)
            {
                velocityPlayer.y = 0f;
            }

            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);

            if (grounded && Input.GetButtonDown("Jump"))
            {
                velocityPlayer.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            velocityPlayer.y += gravityValue * Time.deltaTime;

            controller.Move(velocityPlayer * Time.deltaTime);
        }
    }
}
