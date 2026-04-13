using UnityEngine;

public class OrbitBinaryCenter : MonoBehaviour
{
    [Tooltip("Drag the Empty GameObject representing the center of the universe here.")]
    public Transform binaryCenter;
    
    [Tooltip("How fast the sun orbits the center.")]
    public float orbitSpeed = 5f;

    void Update()
    {
        if (binaryCenter != null)
        {
            // Rotates the sun around the center point on a flat Y plane
            transform.RotateAround(binaryCenter.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Binary Center not assigned to " + gameObject.name);
        }
    }
}