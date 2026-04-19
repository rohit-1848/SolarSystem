using UnityEngine;

public class JoystickAnimator : MonoBehaviour
{
    public Transform handle;
    public float maxAngle = 30f;
    public float smoothSpeed = 10f;

    void Update()
    {
        float inputX = 0f;
        float inputY = 0f;

        Vector2 rightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        inputX = rightStick.x;
        inputY = rightStick.y;

        if (Input.GetKey(KeyCode.O)) inputX = -1f;
        if (Input.GetKey(KeyCode.K)) inputX = 1f;
        if (Input.GetKey(KeyCode.L)) inputY = 1f;
        if (Input.GetKey(KeyCode.P)) inputY = -1f;

        float pitch = inputY * maxAngle;
        float roll = inputX * -maxAngle;

        Quaternion targetRot = Quaternion.Euler(pitch, 0f, roll);
        handle.localRotation = Quaternion.Slerp(handle.localRotation, targetRot, Time.deltaTime * smoothSpeed);
    }
}