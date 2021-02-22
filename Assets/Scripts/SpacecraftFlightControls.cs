using System;
using UnityEngine;

public class SpacecraftFlightControls : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float torque;

    [SerializeField]
    private float maxSpinSpeed;

    [SerializeField]
    private float maxVelocity;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private GameObject flightStick;

    [SerializeField]
    private GameObject throttle;

    [SerializeField]
    private GameObject dragButton;

    [SerializeField]
    private bool disablePitch = false;

    [SerializeField]
    private bool disableRoll = false;

    [SerializeField]
    private bool disableYaw = false;

    [SerializeField]
    private bool disableThrottle = false;

    [SerializeField]
    private float pitchTolerance = -0.05f;

    [SerializeField]
    private float yawTolderance = -0.05f;

    [SerializeField]
    private float rollTolerance = -0.5f;

    private float maxDrag = 3f;
    private float minDrag = 0f;
    private float minAngularDrag = 0f;
    private float maxAngularDrag = 100f;

    private FlightStick fs;
    private DragButton dragButtonTriggerZone;

    private void Awake()
    {
        fs = flightStick.GetComponent<FlightStick>();
        dragButtonTriggerZone = dragButton.transform.Find("TriggerZone").GetComponent<DragButton>();
    }

    private void Control(Vector3 controlVector, float tolerance, bool disabled, Func<float> fn)
    {
        if (disabled) return;

        float diff = fn();

        float controlRotationSpeed = 0f;
        if (diff > Math.Abs(tolerance) || diff < tolerance)
            controlRotationSpeed = diff;

        rb.AddRelativeTorque(controlVector * controlRotationSpeed * torque);

        Vector3 spin = rb.angularVelocity.normalized;
        rb.angularVelocity = spin * maxSpinSpeed;
    }

    private void Pitch()
    {
        Control(Vector3.forward, pitchTolerance, disablePitch, fs.LateralAxis);
    }

    private void Roll()
    {
        Control(Vector3.right, rollTolerance, disableRoll, fs.VerticalAxis);
    }

    private void Yaw()
    {
        Control(Vector3.up, yawTolderance, disableYaw, fs.LongitudinalAxis);        
    }

    private void FlightControl()
    {
        Pitch();
        Roll();
        Yaw();
    }

    private void Throttle()
    {
        if (disableThrottle) return;

        speed = throttle.GetComponent<ThrottleControl>().ThrottleValue * maxVelocity;
        rb.AddForce(transform.right * -speed);
    }

    private void Drag()
    {
        bool isDragButtonPressed = dragButtonTriggerZone.IsPressed();

        rb.drag = isDragButtonPressed ? maxDrag : minDrag;
        rb.angularDrag = isDragButtonPressed ? maxAngularDrag : minAngularDrag;
    }

    private void FixedUpdate()
    {
        Throttle();
        FlightControl();
        Drag();
    }
}
