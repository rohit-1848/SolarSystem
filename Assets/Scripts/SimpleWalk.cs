using UnityEngine;

public class SimpleWalk : MonoBehaviour
{
    public float walkSpeed = 3f;

    void Update()
    {
        // Gets W/S and A/D key inputs
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");

        // Moves the player relative to where they are looking
        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * walkSpeed * Time.deltaTime;
    }
}