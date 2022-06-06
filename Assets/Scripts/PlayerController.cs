using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private CharacterController charController;
    private GameManager gameManager;

    public GameObject cam;

    private Quaternion currentRotation = new Quaternion();

    public float walkSpeed = 4.0f;
    private float jumpVel = 10f;
    private float yVel = 0f;
    private float fireRate = 1.0f;
    private float shotDamage = 5.0f;
    private float critChance = 0;
    private float critMult = 4;
    public float health = 100;
    private float regen = 1;
    public float fireDamage = 0;
    
    private int jumps = 2;
    private int jumpsLeft = 2;
    private int shotCount = 1;


    private bool incendiary = false;
    private bool explosive = false;
    private bool lifeSteal = false;
    public bool slowed = false;
    

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            
        }
    }
}
