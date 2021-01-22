using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabFlightStick : Grabbable
{
    public override void Grab(GrabItem item)
    {
        GameObject collidingObject = item.GetCollidingObject();
        if (collidingObject != null && collidingObject.tag == "FlightStick")
        {
            Transform anchor =
                item.handType == XRNode.RightHand
                ? collidingObject.transform.GetChild(0).GetChild(0).GetChild(0)
                : collidingObject.transform.GetChild(0).GetChild(0).GetChild(1);
        
            collidingObject.GetComponent<FlightStick>().SetHand(item.hand);
            item.handModel.position = anchor.position;
            item.handModel.rotation = anchor.rotation;

            item.handModel.SetParent(anchor);
        }
    }

    public override void Release(GrabItem item)
    {
        GameObject objectInHand = item.GetObjectInHand();
        if (objectInHand != null && objectInHand.tag == "FlightStick")
        {
            objectInHand.GetComponent<FlightStick>().ReleaseHand();
            item.handModel.SetParent(item.transform);
            item.handModel.localPosition = Vector3.zero;
            item.handModel.localEulerAngles = new Vector3(-10, 0, -90);
        }
    }
}
