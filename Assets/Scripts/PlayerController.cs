using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private CharacterController charController;
    private GameManager gameManager;

    public GameObject cam;
    public GameObject bullet;
    public GameObject HUD;

    public AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip shoot;

    private Quaternion currentRotation = new Quaternion();

    public float walkSpeed = 4.0f;
    private float jumpVel = 10f;
    private float yVel = 0f;

    public float fireDelay = 0f;
    public float fireRate = 0.05f;

    public float shotDamage = 5.0f;
    public float critChance = 5f;
    public float critMult = 4;
    public float maxHealth = 100;
    public float health = 100;
    private float regen = 0.8f;
    public float initialRegenDelay = 5f;
    public float regenCountdown = 5f;
    public float regenDelay = 0.8f;
    
    private int jumps = 1;
    private int jumpsLeft = 1;

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

        //movement stuff
        currentRotation.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
        transform.rotation = currentRotation;
        float Hinput = Input.GetAxisRaw("Horizontal");
        float Vinput = Input.GetAxisRaw("Vertical");
        Vector3 movement = ((transform.forward * Vinput) + (transform.right * Hinput))*walkSpeed;
        movement = Vector3.ClampMagnitude(movement, walkSpeed);

        //jump if you are on the ground
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

            //if you are in the air and press space then jump again only if you still have your double jump.
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                yVel = jumpVel;
                jumpsLeft--;
            }
        }

        movement.y += yVel;
        charController.Move(movement * Time.deltaTime);

        //Stuff handling how life regen works; essentially if you are damaged the counter resets and you have to wait 5 more seconds.
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

        //This is how the gun works
        if(fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
        if(Input.GetMouseButton(0))
        {
            if(fireDelay <= 0)
            {
                fireDelay = fireRate;
                
                Vector3 camPos = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
                Vector3 spread = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
                Quaternion camRot = new Quaternion();
                camRot.eulerAngles = cam.transform.rotation.eulerAngles + spread;
                Instantiate(bullet, new Vector3(camPos.x, camPos.y - 0.2f, camPos.z), camRot);
                audioSource.PlayOneShot(shoot);
            }
        }

        if(health <= 0)
        {
            HUD.SetActive(false);
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
