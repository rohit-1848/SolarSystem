using UnityEngine;

[ExecuteAlways]
public class ScatterOnPlanet : MonoBehaviour
{
    [Header("Planet Settings")]
    public Transform planetCenter; 
    public float planetRadius = 100f; // Adjust this to match your Neptune scale

    [Header("Click this to scatter!")]
    public bool scatterNow = false;

    void Update()
    {
        if (scatterNow && planetCenter != null)
        {
            // This loops through every single rock inside the folder
            foreach (Transform item in transform)
            {
                // 1. Pick a completely random direction in 360-degree 3D space
                Vector3 randomDirection = Random.onUnitSphere;

                // 2. Move the rock to the surface of the planet in that direction
                item.position = planetCenter.position + (randomDirection * planetRadius);

                // 3. Rotate the rock so it stands straight up (roots to the core)
                item.up = randomDirection;

                // 4. Give it a random twist so the rocks don't all look identical
                item.Rotate(0, Random.Range(0f, 360f), 0, Space.Self);
            }

            // Turn the checkbox off automatically when done
            scatterNow = false; 
        }
    }
}