using UnityEngine;

public class DragButton : MonoBehaviour
{
    [SerializeField]
    private Transform upTransform;

    [SerializeField]
    private Transform downTransform;

    [SerializeField]
    private Transform buttonMesh;

    private Vector3 originalHandPosition;
    private float magnitudeUpAndDown;
    private bool isButtonPressed = false;

    private void Start()
    {
        magnitudeUpAndDown = downTransform.localPosition.y - upTransform.localPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            originalHandPosition = other.transform.localPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            float yDiff = other.transform.localPosition.y - originalHandPosition.y;
            yDiff = Mathf.Clamp(yDiff, magnitudeUpAndDown, 0);
            buttonMesh.localPosition = new Vector3(upTransform.localPosition.x, upTransform.localPosition.y + yDiff, upTransform.localPosition.z);

            isButtonPressed = (yDiff == magnitudeUpAndDown) ? true : false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            buttonMesh.localPosition = upTransform.localPosition;

        isButtonPressed = false;
    }

    public bool IsPressed()
    {
        return isButtonPressed;
    }
}
