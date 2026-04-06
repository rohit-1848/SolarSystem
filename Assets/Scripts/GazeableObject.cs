// GazeableObject.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public GameObject associatedObject; // Reference to the associated planet GameObject

    public virtual void OnGazeEnter(RaycastHit hitInfo)
    {
        Debug.Log("Gaze entered on " + gameObject.name);
    }

    public virtual void OnGaze(RaycastHit hitInfo)
    {
        Debug.Log("Gaze hold on " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        Debug.Log("Gaze exited on " + gameObject.name);
    }
}
