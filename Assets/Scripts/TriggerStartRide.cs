using UnityEngine;
using Unity.Cinemachine;

public class TriggerStartRide : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag the Dolly Cart object here")]
    public CinemachineSplineCart cartComponent;

    private bool rideStarted = false;

    void Start()
    {
        // Ensure the engine is off at the start
        if (cartComponent != null)
        {
            cartComponent.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (rideStarted) return;

        // Check if the object entering the box has the "Player" tag
        if (other.CompareTag("Player"))
        {
            rideStarted = true;
            
            // Turn the cart engine ON. No teleporting, no moving the rig!
            cartComponent.enabled = true;

            Debug.Log("Player entered the Trigger Box! Ride Auto-Started!");
        }
    }
}