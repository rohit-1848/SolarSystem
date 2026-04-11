using UnityEngine;

public class SpaceshipFlight : MonoBehaviour
{
    [Header("Flight Settings")]
    public float moveSpeed = 2000f; // Keeping it fast for space
    public float turnSpeed = 100f;

    void Update()
    {
        // ==========================================
        // 1. VR CONTROLLER INPUT (Natively supported by Meta Simulator & Headset)
        // ==========================================
        
        // Left Thumbstick: Push Up/Down to move Forward/Backward
        Vector2 leftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (leftStick.y != 0) 
        {
            transform.Translate(Vector3.forward * leftStick.y * moveSpeed * Time.deltaTime);
        }

        // Right Thumbstick: Push Left/Right to Turn, Push Up/Down to move Up/Down
        Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (rightStick.x != 0) 
        {
            transform.Rotate(Vector3.up, rightStick.x * turnSpeed * Time.deltaTime);
        }
        if (rightStick.y != 0) 
        {
            transform.Translate(Vector3.up * rightStick.y * moveSpeed * Time.deltaTime);
        }

        // ==========================================
        // 2. KEYBOARD BACKUP (For Editor Testing)
        // ==========================================
        
        // Forward / Backward (I and K)
        if (Input.GetKey(KeyCode.I)) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.K)) transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        
        // Steer Left / Right (J and L)
        if (Input.GetKey(KeyCode.J)) transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.L)) transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        // Move Up / Down (R and F)
        if (Input.GetKey(KeyCode.R)) transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.F)) transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}