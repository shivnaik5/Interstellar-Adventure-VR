using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    public XRNode m_leftNode;
    public XRNode m_rightNode;

    private static InputDevice leftController;
    private static InputDevice rightController;

    void Awake()
    {
        leftController = InputDevices.GetDeviceAtXRNode(m_leftNode);
        rightController = InputDevices.GetDeviceAtXRNode(m_rightNode);
    }

    public static void VibrateLeft(float strength)
    {
        leftController.SendHapticImpulse(0, strength, 0.1f);
    }

    public static void VibrateRight(float strength)
    {
        rightController.SendHapticImpulse(0, strength, 0.1f);
    }
}
