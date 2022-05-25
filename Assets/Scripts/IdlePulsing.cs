using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePulsing : MonoBehaviour
{
    private float currentScale;
    private float originalScale;
    private bool expanding = true;
    private float targetScale;
    public float scaleIncrease = 0.5f;
    public float scaleSpeed = 1;
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
        currentScale = originalScale;
        targetScale = originalScale + scaleIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        if(expanding)
        {
            if(currentScale >= targetScale)
            {
                expanding = false;
            }
            currentScale = Mathf.MoveTowards(currentScale, targetScale, scaleSpeed * Time.deltaTime);
        } else
        {
            if(currentScale <= originalScale)
            {
                expanding = true;
            }
            currentScale = Mathf.MoveTowards(currentScale, originalScale, scaleSpeed * Time.deltaTime);
        }
        gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
