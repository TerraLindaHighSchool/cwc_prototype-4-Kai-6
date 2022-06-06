using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private CharacterController controller;
    private float rotSpeed = 360f;
    private float yVel;
    public Animator anim;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        controller = GetComponent<CharacterController>();
        
    }


    void Update()
    {
        if (!gameManager.gameActive) return;
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        transform.LookAt(playerPos);
        Vector3 moveVector = transform.forward * speed;
        //transform.rotation = Quaternion.FromToRotation(transform.forward, player.transform.position);
        RaycastHit hit;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -transform.up, out hit, 100);
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        transform.rotation = rot;
            
        if(!controller.isGrounded)
        {
            yVel -= 9.81f;
        } else
        {
            yVel /= 2;
        }
        moveVector.y += yVel;
        if (Vector3.Distance(transform.position, player.transform.position) > 0.5)
        {
            controller.Move(moveVector * Time.deltaTime);
        }
    }
}