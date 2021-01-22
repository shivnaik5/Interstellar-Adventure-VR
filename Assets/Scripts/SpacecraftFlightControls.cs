using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpacecraftFlightControls : MonoBehaviour
{
    public float speed = 50f;
    public float rotationSpeed = 50f;

    public float torque = 50f;

    public float maxVelocity = 100f;

    public Rigidbody rb;

    public GameObject flightStick;

    public GameObject throttle;

    public GameObject dragButton;

    public bool disablePitch = false;
    public bool disableRoll = false;
    public bool disableYaw = false;
    public bool disableThrottle = false;

    public float pitchTolerance = -0.05f;
    public float yawTolderance = -0.05f;
    public float rollTolerance = -0.5f;

    void Control(Vector3 controlVector, float tolerance, bool disabled, Func<float> fn)
    {
        if (disabled) return;

        float diff = fn();

        float controlRotationSpeed = 0f;
        if (diff > Math.Abs(tolerance))
        {
            controlRotationSpeed = rotationSpeed;
        }
        else if (diff < tolerance)
        {
            controlRotationSpeed = rotationSpeed * -1;
        }

        transform.Rotate(controlVector, controlRotationSpeed * Time.deltaTime);
    }

    void Pitch()
    {
        Control(Vector3.forward, pitchTolerance, disablePitch, flightStick.GetComponent<FlightStick>().LateralAxis);
    }

    void Roll()
    {
        Control(Vector3.right, rollTolerance, disableRoll, flightStick.GetComponent<FlightStick>().VerticalAxis);
    }

    void Yaw()
    {
        Control(Vector3.up, yawTolderance, disableYaw, flightStick.GetComponent<FlightStick>().LongitudinalAxis);        
    }

    void Throttle()
    {
        if (disableThrottle) return;
    
        speed = throttle.GetComponent<ThrottleControl>().throttleValue * maxVelocity;
        rb.AddForce(transform.right * -speed);
    }

    void Drag()
    {
        bool isDragButtonPressed = dragButton.transform.Find("TriggerZone").GetComponent<DragButton>().IsPressed();
        if (isDragButtonPressed)
        {
            rb.drag = 3;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void Update()
    {
        Throttle();
        Pitch();
        Roll();
        Yaw();
        Drag();
    }
}
