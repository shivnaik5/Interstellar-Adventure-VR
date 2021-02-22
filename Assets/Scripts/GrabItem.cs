using UnityEngine;
using UnityEngine.XR;

public class GrabItem : MonoBehaviour
{
    [SerializeField]
    private XRNode handType;

    [SerializeField]
    private Transform handModel;

    [SerializeField]
    private Transform hand;

    private GameObject collidingObject;
    private GameObject objectInHand;

    private void Update()
    {
        bool isGripDown = IsGripDown();

        if (isGripDown && collidingObject)
            GrabObject();

        if (!isGripDown && objectInHand)
            ReleaseObject();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject && other.GetComponent<Grabbable>())
            collidingObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGripDown()) return;

        if (objectInHand)
            ReleaseObject();

        collidingObject = null;
    }

    private bool IsGripDown()
    {
        InputDevice inputHand = InputDevices.GetDeviceAtXRNode(handType);
        
        bool isGripDown = false;
        inputHand.TryGetFeatureValue(CommonUsages.gripButton, out isGripDown);

        return isGripDown;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        if (collidingObject.GetComponent<Grabbable>())
            collidingObject.GetComponent<Grabbable>().Grab(this);
    }

    private void ReleaseObject()
    {
        if (collidingObject.GetComponent<Grabbable>())
            objectInHand.GetComponent<Grabbable>().Release(this);

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

    public XRNode HandType
    {
        get
        {
            return handType;
        }
    }

    public Transform Hand
    {
        get
        {
            return hand;
        }
    }

    public Transform HandModel
    {
        get
        {
            return handModel;
        }
    }
}
