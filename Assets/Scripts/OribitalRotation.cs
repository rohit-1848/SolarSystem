using UnityEngine;

public class OrbitalRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}