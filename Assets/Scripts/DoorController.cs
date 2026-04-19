using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorTransform; 
    public Vector3 openPositionOffset = new Vector3(0, 2.5f, 0); 
    public float speed = 2f;

    [Header("Teleport Settings")]
    public Transform playerTransform; // The OVRCameraRig
    public Transform cockpitInsidePoint; 

    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        if (doorTransform != null)
        {
            closedPosition = doorTransform.localPosition;
            openPosition = closedPosition + openPositionOffset;
        }
    }

    void Update()
    {
        Vector3 targetPos = isOpen ? openPosition : closedPosition;
        doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, targetPos, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isOpen = true;

            if (playerTransform != null && cockpitInsidePoint != null)
            {
                // 1. Teleport to the seat
                playerTransform.position = cockpitInsidePoint.position;
                playerTransform.rotation = cockpitInsidePoint.rotation;

                // 2. Buckle up! (Make the player a child of the ship)
                playerTransform.SetParent(cockpitInsidePoint);

                // 3. KILL THE VR WALKING (So thumbsticks only control the ship)
                // This disables any character controllers or locomotion on the player
                CharacterController cc = playerTransform.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;

                // If you are using an OVRPlayerController, this turns it off
                MonoBehaviour[] allScripts = playerTransform.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in allScripts)
                {
                    if (script.GetType().Name == "OVRPlayerController")
                    {
                        script.enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isOpen = false; 
        }
    }
}