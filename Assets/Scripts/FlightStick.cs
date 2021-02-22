using UnityEngine;

public class FlightStick : MonoBehaviour
{
    private bool isGrabbed;

    private Quaternion originalLocalRotation;
    private Quaternion originalTwistLocalRotation;

    private Vector3? originalHandPosition;
    private Vector3? originalHandRotation;

    private Transform hand;

    private float xDiff;
    private float zDiff;
    private float zRotationDiff;

    [SerializeField]
    public Transform twistPivot;

    private void Start()
    {
        originalLocalRotation = transform.localRotation;
        originalTwistLocalRotation = twistPivot.localRotation;

        originalHandPosition = null;
        originalHandRotation = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isGrabbed)
            {
                transform.LookAt(hand.position, transform.up);
                twistPivot.localRotation = Quaternion.Euler(0, 0, GetZRotationDiff());

                JoyStickFeedback();
            }
            else
            {
                transform.localRotation = originalLocalRotation;
                twistPivot.localRotation = originalTwistLocalRotation;
            }
        }
    }

    private void CalculateHandRotationDiff()
    {
        xDiff = hand.localPosition.x - ((Vector3)originalHandPosition).x;
        zDiff = hand.localPosition.z - ((Vector3)originalHandPosition).z;
    }

    private void JoyStickFeedback()
    {
        CalculateHandRotationDiff();
        HapticFeedback.VibrateRight((Mathf.Abs(xDiff) + Mathf.Abs(zDiff)) * 2);
    }

    private float GetZRotationDiff()
    {
        float originalHandY = ((Vector3)originalHandRotation).y;
        float zRotDiff = hand.localEulerAngles.y - originalHandY;

        zRotationDiff = ((zRotDiff > -180) ? zRotDiff : ((360 - originalHandY) + (360 + zRotDiff))) * Mathf.Deg2Rad;
        zRotationDiff /= 10;

        return zRotDiff;
    }

    public float VerticalAxis()
    {
        if (!isGrabbed) zRotationDiff = 0.0f;
        
        return zRotationDiff;
    }

    public float LongitudinalAxis()
    {
        if (!isGrabbed) xDiff = 0.0f;
    
        return xDiff;
    }

    public float LateralAxis()
    {
        if (!isGrabbed) zDiff = 0.0f;
    
        return zDiff;
    }

    public void SetHand(Transform grabHand)
    {
        if (grabHand != null && originalHandPosition == null)
        {
            hand = grabHand;
            isGrabbed = true;
            originalHandPosition = grabHand.localPosition;
            originalHandRotation = grabHand.localEulerAngles;
        }
    }

    public void ReleaseHand()
    {
        hand = null;
        isGrabbed = false;
        originalHandPosition = null;
        originalHandRotation = null;
    }
}
