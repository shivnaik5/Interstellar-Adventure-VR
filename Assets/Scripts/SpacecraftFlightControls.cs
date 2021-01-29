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
    public float maxSpinSpeed;

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

    public float maxDrag = 3f;
    public float minDrag = 0f;
    
    void Control(Vector3 controlVector, float tolerance, bool disabled, Func<float> fn)
    {
        if (disabled) return;

        float diff = fn();

        float controlRotationSpeed = 0f;
        if (diff > Math.Abs(tolerance) || diff < tolerance)
        {
            controlRotationSpeed = diff;
        }

        rb.AddRelativeTorque(controlVector * controlRotationSpeed * torque);

        Vector3 spin = rb.angularVelocity.normalized;
        rb.angularVelocity = spin * maxSpinSpeed;
    }

    void Pitch(FlightStick fs)
    {
        Control(Vector3.forward, pitchTolerance, disablePitch, fs.LateralAxis);
    }

    void Roll(FlightStick fs)
    {
        Control(Vector3.right, rollTolerance, disableRoll, fs.VerticalAxis);
    }

    void Yaw(FlightStick fs)
    {
        Control(Vector3.up, yawTolderance, disableYaw, fs.LongitudinalAxis);        
    }

    void FlightControl()
    {
        FlightStick fs = flightStick.GetComponent<FlightStick>();
        Pitch(fs);
        Roll(fs);
        Yaw(fs);
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
        rb.drag = isDragButtonPressed ? maxDrag : minDrag;
    }

    void FixedUpdate()
    {
        Throttle();
        FlightControl();
        Drag();
    }
}
