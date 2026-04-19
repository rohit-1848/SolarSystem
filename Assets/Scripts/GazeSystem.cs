using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeSystem : MonoBehaviour
{
    public GameObject reticle;
    public Color inactiveReticleColor = Color.gray;
    public Color activeReticleColor = Color.green;

    private GazeableObject currentGazeObject;
    private GazeableObject currentSelectedObject;
    private RaycastHit lastHit;

    private float gazeDuration = 0f;
    private float requiredGazeDuration = 0.5f; // Adjust as needed
    public GameObject canvas4;

    private void Start()
    {
        SetReticleColor(inactiveReticleColor);
    }

    private void Update()
    {
        ProcessGaze();
        CheckForInput(lastHit);
    }

    private void ProcessGaze()
    {
        Ray raycastRay = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 200);

        if (Physics.Raycast(raycastRay, out hitInfo))
        {
            GameObject hitObj = hitInfo.collider.gameObject;
            GazeableObject gazeObj = hitObj.GetComponent<GazeableObject>();

            if (gazeObj != null)
            {
                if (gazeObj != currentGazeObject)
                {
                    ClearCurrentObject();
                    currentGazeObject = gazeObj;
                    currentGazeObject.OnGazeEnter(hitInfo);
                    SetReticleColor(activeReticleColor);
                    gazeDuration = 0f; // Reset gaze duration
                }
                else
                {
                    gazeDuration += Time.deltaTime; // Increment gaze duration

                    if (gazeDuration >= requiredGazeDuration)
                    {
                        if (currentGazeObject.name == "Decrease Tilt")
                        {
                            DecreaseTilt(currentGazeObject.associatedObject);
                        }
                        else if (currentGazeObject.name == "Increase Tilt")
                        {
                            IncreaseTilt(currentGazeObject.associatedObject);
                        }
                        else
                        {
                            MovePlayerToFrontOfTarget(currentGazeObject.associatedObject);
                        }
                    }

                    currentGazeObject.OnGaze(hitInfo);
                }

                lastHit = hitInfo;
            }
            else
            {
                ClearCurrentObject();
            }
        }
        else
        {
            ClearCurrentObject();
        }
    }

    private void CheckForInput(RaycastHit hitinfo)
    {
        // No need to check for input in this implementation
    }

    private void SetReticleColor(Color reticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void ClearCurrentObject()
    {
        if (currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveReticleColor);
            currentGazeObject = null;
            gazeDuration = 0f; // Reset gaze duration when leaving the object
        }
    }

    void DecreaseTilt(GameObject earthObject)
    {
        // Access the RotateAround component attached to the Earth object
        RotateAround rotateAroundScript = earthObject.GetComponent<RotateAround>();

        float axisTiltY = rotateAroundScript.GetAxisTiltY();
        // Calculate the tilt angle using the formula: arccos(1 / sqrt(1 + y^2))
        float tiltAngle = Mathf.Acos(1f / Mathf.Sqrt(1f + axisTiltY * axisTiltY)) * Mathf.Rad2Deg;

        // Method to set the axisTiltY value
        rotateAroundScript.SetAxisTiltY(axisTiltY + 0.01f);

        // Find the Text component named "Current Tilt Text"
        Text tiltText = GameObject.Find("Current Tilt Text").GetComponent<Text>();

        // Calculate the text to display based on axisTiltY
        string tiltTextDisplay;
        if (axisTiltY > 0)
        {
            // If axisTiltY is positive, calculate "90 - tilt angle" value
            float ninetyMinusTilt = 90f - tiltAngle;
            tiltTextDisplay = ninetyMinusTilt.ToString("F2") + "\ndegrees";
        }
        else
        {
            // If axisTiltY is non-positive, calculate "90 + tilt angle" value
            float ninetyPlusTilt = 90f + tiltAngle;
            tiltTextDisplay = ninetyPlusTilt.ToString("F2") + "\ndegrees";
        }

        // Update the tilt text
        tiltText.text = tiltTextDisplay;
}

    void IncreaseTilt(GameObject earthObject)
    {
        // Access the RotateAround component attached to the Earth object
        RotateAround rotateAroundScript = earthObject.GetComponent<RotateAround>();

        float axisTiltY = rotateAroundScript.GetAxisTiltY();
        // Calculate the tilt angle using the formula: arccos(1 / sqrt(1 + y^2))
        float tiltAngle = Mathf.Acos(1f / Mathf.Sqrt(1f + axisTiltY * axisTiltY)) * Mathf.Rad2Deg;

        // Method to set the axisTiltY value
        rotateAroundScript.SetAxisTiltY(axisTiltY - 0.01f);

        // Find the Text component named "Current Tilt Text"
        Text tiltText = GameObject.Find("Current Tilt Text").GetComponent<Text>();

        // Calculate the text to display based on axisTiltY
        string tiltTextDisplay;
        if (axisTiltY > 0)
        {
            // If axisTiltY is positive, calculate "90 - tilt angle" value
            float ninetyMinusTilt = 90f - tiltAngle;
            tiltTextDisplay = ninetyMinusTilt.ToString("F2") + "\ndegrees";
        }
        else
        {
            // If axisTiltY is non-positive, calculate "90 + tilt angle" value
            float ninetyPlusTilt = 90f + tiltAngle;
            tiltTextDisplay = ninetyPlusTilt.ToString("F2") + "\ndegrees";
        }

        // Update the tilt text
        tiltText.text = tiltTextDisplay;

        // Find the Text component named "Current Tilt Text"
        Text tiltTextmirror = GameObject.Find("Current Tilt Text").GetComponent<Text>();
        // Update the tilt text
        tiltTextmirror.text = tiltTextDisplay;
    }

    private void MovePlayerToFrontOfTarget(GameObject target)
    {
        // Get the player GameObject (which is the parent of the main camera)
        GameObject player = transform.parent.gameObject;
        canvas4.SetActive(false);
        // Check if the target is "Earth Eclipse"

        // Calculate the direction from the player to the target object
        Vector3 directionToTarget = target.transform.position - player.transform.position;
            Vector3 targetPosition;

        // Check if the target is "Object for Asteroid Belt"
        if (target.name == "Object for Asteroid Belt")
            {
                // Calculate the target position in front of the object with a fixed distance of 2f
                targetPosition = target.transform.position - directionToTarget.normalized * 0.01f;
            }
        else if (target.name == "Earth Eclipse")
            {
                // Set the player's position to the specified coordinates
                targetPosition = new Vector3(9.4f, 0f, -10000f);
            }
        else if (target.name == "Moon Lunar Eclipse")
        {
            // Set the player's position to the specified coordinates
            targetPosition = new Vector3(10.6f, 0f, 10000f);
        }
        else
            {
                // Get the radius of the target object
                float objectRadius = target.transform.localScale.x / 2f; // Assuming the scale is uniform

                // Calculate the desired distance from the target based on the object's radius
                float distanceFromTarget = objectRadius * 6f; // Adjust scaleFactor as needed

                // Calculate the target position in front of the object
                targetPosition = target.transform.position - directionToTarget.normalized * distanceFromTarget;
            }

        // Update the player's position to the target position
        player.transform.position = targetPosition;



        if (target.name == "Object for Earth")
        {
            // Calculate the direction from the player to the Earth
            Vector3 directionToEarth = target.transform.position - player.transform.position;

            canvas4.SetActive(true);

            // Get the canvas's transform
            Transform canvasTransform = canvas4.transform;

            // Calculate the rotation to align the canvas with the direction to Earth
            Quaternion targetRotation = Quaternion.LookRotation(directionToEarth, Vector3.up);

            // Set the canvas's rotation with the full target rotation
            canvasTransform.rotation = targetRotation;
        }


        // Ensure the player continues to move with the object
        player.transform.parent = target.transform;

        // Update the text content based on the target name
        UpdateCanvasText(target.name);
    }


    private void UpdateCanvasText(string targetName)
    {
        Debug.Log("Target Name: " + targetName); // Add this line

        // Find the canvas GameObject
        GameObject canvas = GameObject.Find("Text Canvas");

        // Find the text component
        Text textComponent = canvas.GetComponentInChildren<Text>();

        // Trim the target name to remove leading and trailing spaces
        targetName = targetName.Trim();

        // Update the text content based on the target name
        switch (targetName)
        {
            case "Object for Sun":
                textComponent.text = "The Sun is the star at the center of our solar system, a massive ball of plasma. It provides the majority of the solar system's energy.";
                break;
            case "Object for Mercury":
                textComponent.text = "Mercury is the closest planet to the Sun, a small rocky world with a thin atmosphere. It has a heavily cratered surface.";
                break;
            case "Object for Venus":
                textComponent.text = "Venus is often called Earth's sister planet due to its similar size and composition. However, it has a thick, toxic atmosphere that creates a runaway greenhouse effect.";
                break;
            case "Object for Earth":
                textComponent.text = "Earth is the third planet from the Sun and the only known planet to support life. It is a diverse and dynamic world with a wide range of features.";
                break;
            case "Object for Mars":
                textComponent.text = "Mars is often referred to as the Red Planet due to its reddish appearance. It is a cold, dry planet with a thin atmosphere and a surface dominated by craters, volcanoes, and a giant canyon.";
                break;
            case "Moon":
                textComponent.text = "The Moon is Earth's natural satellite, the largest and most massive object in the Earth's orbit. It has a significant impact on Earth's tides and climate.";
                break;
            case "Object for Jupiter":
                textComponent.text = "Jupiter is the largest planet in the Solar System, a gas giant with a massive, swirling atmosphere. It has a powerful magnetic field that protects the planet.";
                break;
            case "Object for Saturn":
                textComponent.text = "Saturn is known for its iconic ring system, which is composed of billions of small chunks of ice and rock. It is the second-largest planet in the Solar System.";
                break;
            case "Object for Uranus":
                textComponent.text = "Uranus is the seventh planet from the Sun and the third-largest planet. It is unique in that it rotates on its side, resulting in extreme seasons.";
                break;
            case "Object for Neptune":
                textComponent.text = "Neptune is the farthest planet from the Sun, a gas giant with a dark, stormy atmosphere. It has the fastest winds in the Solar System.";
                break;
            case "Object for Pluto":
                textComponent.text = "Pluto was once considered the ninth planet but was reclassified as a dwarf planet in 2006. It is a small, icy world located in the Kuiper Belt.";
                break;
            case "Object for Asteroid Belt":
                textComponent.text = "The Asteroid Belt lies between the orbits of Mars and Jupiter, a region containing a large number of rocky objects known as asteroids.";
                break;
            case "Earth Eclipse":
                textComponent.text = "During a solar eclipse, the moon passes between the sun and Earth, blocking the sunlight and casting a shadow on the Earth's surface.";
                break;
            case "Moon Lunar Eclipse":
                textComponent.text = "During a lunar eclipse, the Earth's shadow falls on the Moon, giving it a reddish hue known as a \"blood moon.\"";
                break;
            default:
                textComponent.text = "Welcome to our Solar System!!!";
                break;
        }
    }


}