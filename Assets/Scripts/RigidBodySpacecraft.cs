using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RigidBodySpacecraft : MonoBehaviour
{
    public float torque = 100f;
    public float thrust = 100f;
    public float speed = 50f;

    public Rigidbody rb;

    private InputDevice rightHand;

    void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void FixedUpdate()
    {
        Thrust();
        Drag();
    }

    void Thrust()
    {
        if (Input.GetButtonDown("Right A Button Press"))
        {
            rb.AddForce(transform.right * -speed * 5);
        }
    }

    void Drag()
    {
        if (Input.GetButtonDown("Left X Button Press"))
        {
            rb.drag = 3;
        }
        else if (Input.GetButtonUp("Left X Button Press"))
        {
            rb.drag = 0;
        }
    }

    void RightJoystickMove()
    {
        InputFeatureUsage<Vector2> primary2DAxis = CommonUsages.primary2DAxis;
        if (rightHand.TryGetFeatureValue(primary2DAxis, out Vector2 primary2DValue) && primary2DValue != Vector2.zero)
        {
            Debug.Log("primary2daxisclick is pressed " + primary2DValue);
        }

    }
}
