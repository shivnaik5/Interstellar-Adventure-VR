using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public virtual void Grab(GrabItem item)
    {
        GameObject collidingObject = item.GetCollidingObject();
        collidingObject.transform.SetParent(this.transform);
        collidingObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public virtual void Release(GrabItem item)
    {
        GameObject objectInHand = item.GetObjectInHand();
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.transform.SetParent(objectInHand.GetComponent<Grabbable>().Parent.transform);
    }
}
