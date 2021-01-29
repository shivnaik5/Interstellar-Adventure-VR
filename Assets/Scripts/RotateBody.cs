using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBody : MonoBehaviour
{
    public float rotateTime = 0.002f;
    public float orbitSpeed = 1;

    public Transform sun;

    public bool disableOrbit = false;
    public bool disableRotation = false;

    void Update()
    {
        if (!disableRotation)
        {
            float localRotation = rotateTime > 0 ? -(360 / (rotateTime * 60 * 60)) * Time.deltaTime : 0;
            transform.Rotate(0, localRotation, 0, Space.Self);
        }
 

        if (!disableOrbit)
        {
            transform.RotateAround(sun.position, Vector3.up, -orbitSpeed * Time.deltaTime);
        }
    }
}
