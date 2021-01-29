using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravitationalConstant = 5;
    public float cutoffDistance = 500f;

    public Rigidbody rb;
    
    public Rigidbody spacecraftRigidbody;

    public bool gravityWell = true;

    void FixedUpdate()
    {
        if (!gravityWell) return;

        Vector3 direction = transform.position - spacecraftRigidbody.transform.position;
        float distance = direction.magnitude;

        if (distance >= cutoffDistance)
            return;

        float gravityMagnitude = gravitationalConstant * (rb.mass * spacecraftRigidbody.mass) / Mathf.Pow(distance, 2); // GMm/r^2
        Vector3 gravityVector = gravityMagnitude * direction.normalized;

        spacecraftRigidbody.AddForce(gravityVector, ForceMode.Acceleration);
    }
}
