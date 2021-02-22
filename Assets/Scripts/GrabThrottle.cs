using UnityEngine;
using UnityEngine.XR;
public class GrabThrottle : Grabbable
{
    public override void Grab(GrabItem item)
    {
        GameObject collidingObject = item.GetCollidingObject();
        if (collidingObject != null && collidingObject.tag == "Throttle")
        {
            collidingObject.GetComponent<ThrottleControl>().IsGrabbed = true;

            Transform anchor =
                item.HandType == XRNode.RightHand
                ? collidingObject.transform.GetChild(0)
                : collidingObject.transform.GetChild(1);

            item.HandModel.position = anchor.position;
            item.HandModel.rotation = anchor.rotation;

            item.HandModel.SetParent(anchor);
        }
    }

    public override void Release(GrabItem item)
    {
        GameObject objectInHand = item.GetObjectInHand();
        if (objectInHand != null && objectInHand.tag == "Throttle")
        {
            objectInHand.GetComponent<ThrottleControl>().IsGrabbed = false;
            item.HandModel.SetParent(item.transform);
            item.HandModel.localPosition = Vector3.zero;
            item.HandModel.localEulerAngles = new Vector3(-10, 0, -90);
        }
    }
}
