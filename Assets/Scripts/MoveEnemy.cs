using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float rotationPeriod;

    private  float lateralLength = 1;
    private bool isRotating = false;
    private float xDirection = 0;
    private float rotationTime = 0;
    private float radius;
    private float x;
    private Vector3 startPosition;

    private Quaternion beforeRotation;
    private Quaternion afterRotation;


    void Start()
    {
        x = 1;
        radius = lateralLength * Mathf.Sqrt(2f) / 2f;
    }

    void Update()
    {
        if (x != 0 && !isRotating)
        {
            xDirection = x;
            startPosition = transform.position;
            beforeRotation = transform.rotation;
            transform.Rotate(0, 0, xDirection * 90, Space.World);
            afterRotation = transform.rotation;
            transform.rotation = beforeRotation;
            rotationTime = 0;
            isRotating = true;
        }
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            rotationTime += Time.fixedDeltaTime;
            float ratioFactor = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);

            float theta = Mathf.Lerp(0, Mathf.PI / 2f, ratioFactor);
            float xDistance = -xDirection * radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + theta));
            float yDistance = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + theta) - Mathf.Sign(45f * Mathf.Deg2Rad));
            transform.position = new Vector2(startPosition.x + xDistance, startPosition.y + yDistance);
            transform.rotation = Quaternion.Lerp(beforeRotation, afterRotation, ratioFactor);

            if (ratioFactor == 1) {
                isRotating = false;
                xDirection = 0;
                rotationTime = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BordersTag"))  
        {
            x *= -1;
        }  
    }

}
