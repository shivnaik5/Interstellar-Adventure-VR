﻿using UnityEngine;
using UnityEngine.XR;
public class GameManager : MonoBehaviour
{
    void Awake()
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }
}
