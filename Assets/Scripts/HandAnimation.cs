using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimation : MonoBehaviour
{
    public XRNode HandType;
    public Animator HandAnimator;

    private void Update()
    {
        bool grip = false;
        bool trigger = false;
        bool primaryAxisTouch = false;
        bool primaryTouch = false;
        bool secondaryTouch = false;
        float triggerDown = 0;

        InputDevice hand = InputDevices.GetDeviceAtXRNode(HandType);
        hand.TryGetFeatureValue(CommonUsages.gripButton, out grip);
        hand.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        hand.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out primaryAxisTouch);
        hand.TryGetFeatureValue(CommonUsages.primaryTouch, out primaryTouch);
        hand.TryGetFeatureValue(CommonUsages.secondaryTouch, out secondaryTouch);
        hand.TryGetFeatureValue(CommonUsages.trigger, out triggerDown);

        bool thumbDown = primaryAxisTouch || primaryTouch || secondaryTouch;

        float triggerTotal = 0f;
        if (trigger)
        {
            triggerTotal = 0.1f;
        }
        if (triggerDown > 0.1f)
        {
            triggerTotal = triggerDown;
        }

        HandAnimator.SetBool("GrabbingGrip", grip);
        HandAnimator.SetBool("ThumbUp", !thumbDown);
        HandAnimator.SetFloat("TriggerDown", triggerTotal);
    }
}