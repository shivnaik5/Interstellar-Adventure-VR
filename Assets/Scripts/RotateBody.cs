using UnityEngine;

public class RotateBody : MonoBehaviour
{
    [SerializeField]
    private float rotateTime = 0.002f;

    [SerializeField]
    private float orbitSpeed = 1;

    [SerializeField]
    private Transform sun;

    [SerializeField]
    private bool disableOrbit = false;

    [SerializeField]
    private bool disableRotation = false;

    private void Update()
    {
        if (!disableRotation)
        {
            float localRotation = rotateTime > 0 ? -(360 / (rotateTime * 60 * 60)) * Time.deltaTime : 0;
            transform.Rotate(0, localRotation, 0, Space.Self);
        }
 

        if (!disableOrbit)
        {
            transform.RotateAround(sun.position, Vector3.up, -orbitSpeed * Time.deltaTime);
        }
    }
}
