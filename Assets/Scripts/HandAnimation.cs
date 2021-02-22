using UnityEngine;
using UnityEngine.XR;

public class HandAnimation : MonoBehaviour
{
    [SerializeField]
    private XRNode handType;

    [SerializeField]
    private Animator handAnimator;

    private void Update()
    {
        bool grip = false;
        bool trigger = false;
        bool primaryAxisTouch = false;
        bool primaryTouch = false;
        bool secondaryTouch = false;
        float triggerDown = 0;

        InputDevice hand = InputDevices.GetDeviceAtXRNode(handType);
        hand.TryGetFeatureValue(CommonUsages.gripButton, out grip);
        hand.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        hand.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primaryAxisTouch);
        hand.TryGetFeatureValue(CommonUsages.primaryTouch, out primaryTouch);
        hand.TryGetFeatureValue(CommonUsages.secondaryTouch, out secondaryTouch);
        hand.TryGetFeatureValue(CommonUsages.trigger, out triggerDown);

        bool thumbDown = primaryAxisTouch || primaryTouch || secondaryTouch;

        float triggerTotal = 0f;
        if (trigger)
            triggerTotal = 0.1f;

        if (triggerDown > 0.1f)
            triggerTotal = triggerDown;

        handAnimator.SetBool("GrabbingGrip", grip);
        handAnimator.SetBool("ThumbUp", !thumbDown);
        handAnimator.SetFloat("TriggerDown", triggerTotal);
    }
}