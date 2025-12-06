using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;

public class ForceFloorTracking : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SetupTracking());
    }

    IEnumerator SetupTracking()
    {
        // Wait for XR to initialize
        yield return new WaitForSeconds(1f);

        // Try to set floor tracking mode
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetSubsystems(xrDisplaySubsystems);

        foreach (var subsystem in xrDisplaySubsystems)
        {
            Debug.Log($"[ForceFloorTracking] Found display subsystem: {subsystem.GetType().Name}");
        }

        var xrInputSubsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(xrInputSubsystems);

        foreach (var subsystem in xrInputSubsystems)
        {
            Debug.Log($"[ForceFloorTracking] Input subsystem: {subsystem.GetType().Name}");
            Debug.Log($"[ForceFloorTracking] Current tracking mode: {subsystem.GetTrackingOriginMode()}");

            // Try to set floor mode
            if (subsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor))
            {
                Debug.Log("[ForceFloorTracking] Successfully set Floor tracking mode!");
            }
            else
            {
                Debug.Log("[ForceFloorTracking] Failed to set Floor tracking mode");

                // Try device mode as fallback
                if (subsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Device))
                {
                    Debug.Log("[ForceFloorTracking] Set Device tracking mode instead");
                }
            }

            // Recenter
            subsystem.TryRecenter();
        }
    }
}
