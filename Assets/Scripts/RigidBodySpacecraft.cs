using UnityEngine;
using UnityEngine.XR;

/* Testing Oculus Quest 2 controller controls */

public class RigidBodySpacecraft : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    [SerializeField]
    private Rigidbody rb;

    private InputDevice rightHand;

    private float maxDrag = 3f;
    private float minDrag = 0f;

    private float torque = 100f;
    private float thrust = 100f;

    private void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void FixedUpdate()
    {
        Thrust();
        Drag();
    }

    private void Thrust()
    {
        if (Input.GetButtonDown("Right A Button Press"))
            rb.AddForce(transform.right * -speed * 5);
    }

    private void Drag()
    {
        rb.drag = Input.GetButtonDown("Left X Button Press") ? maxDrag : minDrag;
    }

    private void RightJoystickMove()
    {
        InputFeatureUsage<Vector2> primary2DAxis = CommonUsages.primary2DAxis;
        if (rightHand.TryGetFeatureValue(primary2DAxis, out Vector2 primary2DValue) && primary2DValue != Vector2.zero)
        {
            Debug.Log("primary2daxisclick is pressed " + primary2DValue);
        }
    }
}
