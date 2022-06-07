using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private float lifeTime = 2;
    private float currentLife = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLife += Time.deltaTime;
        if(currentLife > lifeTime)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
