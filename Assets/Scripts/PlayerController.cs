﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private CharacterController charController;
    private GameManager gameManager;

    public GameObject cam;
    public GameObject bullet;

    private Quaternion currentRotation = new Quaternion();

    public float walkSpeed = 4.0f;
    private float jumpVel = 10f;
    private float yVel = 0f;

    public float fireDelay = 0f;
    public float fireRate = 1.0f;

    private float shotDamage = 5.0f;
    private float critChance = 5f;
    private float critMult = 4;
    public float maxHealth = 100;
    public float health = 100;
    private float regen = 0.8f;
    private float initialRegenDelay = 5f;
    public float regenCountdown = 5f;
    public float regenDelay = 0.8f;
    
    private int jumps = 2;
    private int jumpsLeft = 2;

    public bool takenDamage = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (!gameManager.gameActive) return;
        currentRotation.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
        transform.rotation = currentRotation;
        float Hinput = Input.GetAxisRaw("Horizontal");
        float Vinput = Input.GetAxisRaw("Vertical");
        Vector3 movement = ((transform.forward * Vinput) + (transform.right * Hinput))*walkSpeed;
        movement = Vector3.ClampMagnitude(movement, walkSpeed);

        if (charController.isGrounded)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVel = jumpVel;
                jumpsLeft = jumps;
            }
        }
        else 
        { 
            
            yVel += -9.81f * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                yVel = jumpVel;
                jumpsLeft--;
            }
        }

        movement.y += yVel;
        charController.Move(movement * Time.deltaTime);

        if(regenCountdown > 0)
        {
            regenCountdown -= Time.deltaTime;
        }

        if((health < maxHealth) && regenCountdown <= 0)
        {
            if (regenDelay <= 0)
            {
                health++;
                regenDelay = regen;
            }
            else
            {
                regenDelay -= Time.deltaTime;
            }
        }

        if(fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
        if(Input.GetMouseButton(0))
        {
            if(fireDelay <= 0)
            {
                fireDelay = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
