using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{

    public Transform target; // the object to rotate around
    public float speed; // the speed of rotation

    private float axisTiltY = 2.2998f; // Default value for axis tilt

    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
            Debug.Log("RotateAround target not specified. Defaulting to parent GameObject");
        }
    }

    // Getter method to access the y-component of directionToSun
    public float GetAxisTiltY()
    {
        return axisTiltY;
    }

    // Method to set the axisTiltY value
    public void SetAxisTiltY(float value)
    {
        axisTiltY = value;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if both target and game object are Earth
        if (target == this.gameObject.transform && target.name == "Earth")
        {
            // Find the position of the Sun
            Transform sunTransform = GameObject.Find("Sun").transform;

            // Calculate the direction from Earth to Sun
            Vector3 directionToSun = (sunTransform.position - transform.position).normalized;

            // Set the y-component of the direction vector using the axisTiltY value
            directionToSun.y = axisTiltY;

            // Draw a line representing the axis
            Debug.DrawLine(transform.position - directionToSun * 4f, transform.position + directionToSun * 4f, Color.white);

            // Rotate around the calculated axis
            transform.RotateAround(transform.position, directionToSun, speed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
        }
    }
}
