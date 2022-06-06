using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private CharacterController controller;
    public Animator anim;

    public float enemyDamage = 2;
    public float speed;
    private float rotSpeed = 60f;
    private float yVel;
    public float myHealth = 50;
    public float attackDelay = 1.5f;
    private float attackCountdown = 1.5f;
    public float distancefrom;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        myHealth = spawnManager.enemyHealth;
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (!gameManager.gameActive) return;

        if(attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }

        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPos);

        Vector3 moveVector = transform.forward * speed;

        if (!controller.isGrounded)
        {
            yVel -= 9.81f;
        } else
        {
            yVel /= 2;
        }

        moveVector.y += yVel;
        distancefrom = Vector3.Distance(transform.position, player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            anim.SetBool("Moving", true);
            controller.Move(moveVector * Time.deltaTime);
        } 
        else
        {
            anim.SetBool("Moving", false);
            if(attackCountdown <= 0)
            {
                anim.SetTrigger("Attack");
                player.GetComponent<PlayerController>().health -= enemyDamage;
                player.GetComponent<PlayerController>().regenCountdown = 5f;
                attackCountdown = attackDelay;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            myHealth -= 10;
            Destroy(other);
            if(myHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}