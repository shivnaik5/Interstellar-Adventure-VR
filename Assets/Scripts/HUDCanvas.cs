using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCanvas : MonoBehaviour
{
    public Transform spacecraft;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(spacecraft.right, Vector3.up);
    }
}
