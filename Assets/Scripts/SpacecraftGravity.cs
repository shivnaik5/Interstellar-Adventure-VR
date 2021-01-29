using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftGravity : MonoBehaviour
{
    public Rigidbody rb;
    public float scale = 10f;

    void Start()
    {
        rb.AddForce(transform.forward * rb.mass * scale, ForceMode.Impulse);
    }
}
