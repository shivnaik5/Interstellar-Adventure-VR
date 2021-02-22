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
                item.HandType == XRNode.RightHand
                ? collidingObject.transform.GetChild(0).GetChild(0).GetChild(0)
                : collidingObject.transform.GetChild(0).GetChild(0).GetChild(1);
        
            collidingObject.GetComponent<FlightStick>().SetHand(item.Hand);
            item.HandModel.position = anchor.position;
            item.HandModel.rotation = anchor.rotation;

            item.HandModel.SetParent(anchor);
        }
    }

    public override void Release(GrabItem item)
    {
        GameObject objectInHand = item.GetObjectInHand();
        if (objectInHand != null && objectInHand.tag == "FlightStick")
        {
            objectInHand.GetComponent<FlightStick>().ReleaseHand();
            item.HandModel.SetParent(item.transform);
            item.HandModel.localPosition = Vector3.zero;
            item.HandModel.localEulerAngles = new Vector3(-10, 0, -90);
        }
    }
}
