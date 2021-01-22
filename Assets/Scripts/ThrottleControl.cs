using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleControl : MonoBehaviour
{
    public Transform throttleStart;
    public Transform throttleEnd;

    public Transform handThrottleStart;

    public GameObject dragButton;

    public bool isGrabbed;

    public float throttleValue;

    void Update()
    {
        bool isDragButtonPressed = dragButton.transform.Find("TriggerZone").GetComponent<DragButton>().IsPressed();
        if (isDragButtonPressed)
        {
            transform.position = throttleStart.position;
            throttleValue = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isGrabbed == true)
        {
            Vector3 direction = throttleEnd.position - throttleStart.position;
            float magnitude = direction.magnitude;
            direction.Normalize();

            Vector3 handStart = other.transform.position - handThrottleStart.position;

            float dotProduct = Vector3.Dot(handStart, direction);
            dotProduct = Mathf.Clamp(dotProduct, 0f, magnitude);
            Vector3 updatedPosition = throttleStart.position + (direction * dotProduct);

            transform.position = new Vector3(updatedPosition.x, updatedPosition.y, updatedPosition.z);

            throttleValue = dotProduct / magnitude;
        }
    }
}
