using UnityEngine;

public class SpacecraftGravity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float scale = 10f;

    void Start()
    {
        rb.AddForce(transform.forward * rb.mass * scale, ForceMode.Impulse);
    }
}
