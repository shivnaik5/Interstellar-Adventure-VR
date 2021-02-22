using UnityEngine;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    [SerializeField]
    private XRNode m_leftNode;

    [SerializeField]
    private XRNode m_rightNode;

    private static InputDevice leftController;
    private static InputDevice rightController;

    private void Awake()
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
