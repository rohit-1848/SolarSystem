using UnityEngine;

// This special tag makes the script run while you are building, without needing to press Play!
[ExecuteAlways]
public class AlignToPlanet : MonoBehaviour
{
    [Header("Planet Settings")]
    public Transform planetCenter; // Drag your blue planet here
    public float planetRadius = 50f; // Change this to match your planet's size

    [Header("Click this box to snap!")]
    public bool snapToSurface = false;

    void Update()
    {
        if (snapToSurface && planetCenter != null)
        {
            // 1. Find the direction from the center of the planet to this object
            Vector3 gravityUp = (transform.position - planetCenter.position).normalized;

            // 2. Move the object exactly to the surface of the sphere
            transform.position = planetCenter.position + (gravityUp * planetRadius);

            // 3. Rotate the object so it stands straight up relative to the planet
            transform.up = gravityUp;

            // 4. Turn the checkbox off automatically
            snapToSurface = false; 
        }
    }
}