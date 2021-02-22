using UnityEngine;

public class PlanetGravity : Gravity
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Spacecraft")
            GravityPull(other.GetComponent<Rigidbody>());
    }
}
