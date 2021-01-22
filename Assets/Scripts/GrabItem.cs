using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabItem : MonoBehaviour
{
    public XRNode handType;
    public Transform handModel;
    public Transform hand;

    private GameObject collidingObject;
    private GameObject objectInHand;

    void Update()
    {
        bool isGripDown = IsGripDown();

        if (isGripDown && collidingObject)
        {
            GrabObject();
        }

        if (!isGripDown && objectInHand)
        {
            ReleaseObject();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject)
        {
            collidingObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsGripDown()) return;

        if (objectInHand)
        {
            ReleaseObject();
        }

        collidingObject = null;
    }

    bool IsGripDown()
    {
        InputDevice inputHand = InputDevices.GetDeviceAtXRNode(handType);
        
        bool isGripDown = false;
        inputHand.TryGetFeatureValue(CommonUsages.gripButton, out isGripDown);

        return isGripDown;
    }

    void GrabObject()
    {
        objectInHand = collidingObject;
        if (collidingObject.GetComponent<Grabbable>())
        {
            collidingObject.GetComponent<Grabbable>().Grab(this);
        }
    }

    void ReleaseObject()
    {
        if (collidingObject.GetComponent<Grabbable>())
        {
            objectInHand.GetComponent<Grabbable>().Release(this);
        }
        objectInHand = null;
    }

    public GameObject GetObjectInHand()
    {
        return objectInHand;
    }

    public GameObject GetCollidingObject()
    {
        return collidingObject;
    }
}
