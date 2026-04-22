using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class SupernovaEvent : MonoBehaviour
{
    public VisualEffect explosionVFX;
    public MeshRenderer starMesh;

    void Start()
    {
        StartCoroutine(Detonate());
    }

    IEnumerator Detonate()
    {
        // Wait 2 seconds
        yield return new WaitForSeconds(2.0f);

        // 1. Hide the sphere
        if (starMesh != null) starMesh.enabled = false;

        // 2. FORCE the explosion to reset and fire immediately
        if (explosionVFX != null)
        {
            explosionVFX.Reinit(); // This wipes any glitches
            explosionVFX.Play();   // This forces the detonation
        }
    }
}