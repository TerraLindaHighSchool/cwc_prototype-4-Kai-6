using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotate : MonoBehaviour
{
    private float currentRotation = 0f;
    public float rotationRate = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, rotationRate * Time.deltaTime, 0), Space.Self);
    }
}
