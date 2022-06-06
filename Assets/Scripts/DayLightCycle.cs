using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightCycle : MonoBehaviour
{
    public float dayMins = 5;
    private float dayTime;
    void Start()
    {
        dayTime = 180/(dayMins * 60);
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(dayTime * Time.deltaTime, 0, 0), Space.Self);
    }
}
