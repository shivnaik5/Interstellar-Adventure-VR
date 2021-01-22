using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VrRigSetup : MonoBehaviour
{
    void Start()
    {
        bool success = XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }
}
