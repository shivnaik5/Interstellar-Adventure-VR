using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragButton : MonoBehaviour
{
    public Transform upTransform;
    public Transform downTransform;

    private Transform hand;

    private Vector3 originalHandPosition;

    private float magnitudeUpAndDown;

    private bool isButtonPressed = false;

    public Transform buttonMesh;
    void Start()
    {
        magnitudeUpAndDown = downTransform.localPosition.y - upTransform.localPosition.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            originalHandPosition = other.transform.localPosition;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            float yDiff = other.transform.localPosition.y - originalHandPosition.y;
            yDiff = Mathf.Clamp(yDiff, magnitudeUpAndDown, 0);
            buttonMesh.localPosition = new Vector3(upTransform.localPosition.x, upTransform.localPosition.y + yDiff, upTransform.localPosition.z);

            isButtonPressed = (yDiff == magnitudeUpAndDown) ? true : false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            buttonMesh.localPosition = upTransform.localPosition;
        }

        isButtonPressed = false;
    }

    public bool IsPressed()
    {
        return isButtonPressed;
    }
}
