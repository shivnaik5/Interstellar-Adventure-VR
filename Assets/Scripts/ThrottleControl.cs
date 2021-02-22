using System.Collections.Generic;
using UnityEngine;

public class ThrottleControl : MonoBehaviour
{
    [SerializeField]
    private Transform throttleStart;

    [SerializeField]
    private Transform throttleEnd;

    [SerializeField]
    private Transform handThrottleStart;

    [SerializeField]
    private GameObject dragButton;

    [SerializeField]
    private List<Texture> m_speeds = new List<Texture>();

    [SerializeField]
    private GameObject spacecraft;

    [SerializeField]
    private Renderer m_rend;

    [ReadOnly] [SerializeField]
    private bool isGrabbed;

    [ReadOnly] [SerializeField]
    private float throttleValue;

    private SpacecraftFlightControls spacecraftFlightControls;
    private DragButton dragButtonComponent;

    private void Awake()
    {
        spacecraftFlightControls = spacecraft.GetComponent<SpacecraftFlightControls>();
        dragButtonComponent = dragButton.transform.Find("TriggerZone").GetComponent<DragButton>();
    }

    private void Update()
    {
        CheckDragButton();
        UpdateThrottleGauge();
    }

    private void OnTriggerStay(Collider other)
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

    private void CheckDragButton()
    {
        bool isDragButtonPressed = dragButtonComponent.IsPressed();
        if (isDragButtonPressed)
        {
            transform.position = throttleStart.position;
            throttleValue = 0;
        }
    }

    private void UpdateThrottleGauge()
    {
        float index = throttleValue * m_speeds.Count;
        index = Mathf.Clamp(Mathf.Ceil(index), 0, m_speeds.Count - 1);

        int intIndex = (int)index;
        m_rend.materials[5].mainTexture = m_speeds[intIndex];
    }

    public bool IsGrabbed
    {
        get
        {
            return isGrabbed;
        }

        set
        {
            isGrabbed = value;
        }
    }

    public float ThrottleValue
    {
        get
        {
            return throttleValue;
        }
    }
}
