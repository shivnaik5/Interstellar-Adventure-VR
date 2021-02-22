using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravitationalConstant = 5;

    [SerializeField]
    private float cutoffDistance = 0f;

    [SerializeField]
    private bool useCutoffDistance = false;

    [SerializeField]
    private bool gravityWell = true;

    [SerializeField]
    private Rigidbody rb;

    protected void GravityPull(Rigidbody otherRigidBody)
    {
        if (!gravityWell)
            return;

        Vector3 direction = transform.position - otherRigidBody.transform.position;
        float distance = direction.magnitude;

        if (useCutoffDistance && distance >= cutoffDistance)
            return;

        float gravityMagnitude = gravitationalConstant * (rb.mass * otherRigidBody.mass) / Mathf.Pow(distance, 2); // GMm/r^2
        Vector3 gravityVector = gravityMagnitude * direction.normalized;

        otherRigidBody.AddForce(gravityVector, ForceMode.Acceleration);
    }
}
