using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private float jumpForce = 3.0f;
    private float fireRate = 1.0f;
    private float shotDamage = 5.0f;
    private float critChance = 0;
    private float critMult = 4;
    private int shotCount = 1;
    public float health = 100;
    private float regen = 1;
    private bool incendiary = false;
    private bool explosive = false;
    private bool lifeSteal = false;
    public bool slowed = false;
    public float fireDamage = 2;
    public GameObject powerupIndicator;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (!gameManager.gameActive) return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
        }
    }
}
