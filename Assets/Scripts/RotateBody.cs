using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBody : MonoBehaviour
{

    public float rotateTime = 0.002f;
    public Transform sun;
    public float orbitSpeed = 1;

    void Update()
    {
        float localRotation = rotateTime > 0 ? -(360 / (rotateTime * 60 * 60)) * Time.deltaTime : 0;
        transform.Rotate(0, localRotation, 0, Space.Self);
        transform.RotateAround(sun.position, Vector3.up, -orbitSpeed * Time.deltaTime);
    }
}
