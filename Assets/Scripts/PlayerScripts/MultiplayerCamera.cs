using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerCamera : NetworkBehaviour
{
    private Camera mainCamera;         // Assign your camera in inspector
    public Vector3 offset = new Vector3(0, 20, 0);  // Camera offset

    private Transform target;

    void Start()
    {
        // Only run on the local player's instance
        if (!IsOwner) return;

        target = transform; // The camera follows this player
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Set camera position initially
        mainCamera.transform.position = target.position + offset;
        mainCamera.transform.LookAt(target);
    }

    void LateUpdate()
    {
        if (!IsOwner || target == null) return;

        // Smooth follow
        mainCamera.transform.position = target.position + offset;
        mainCamera.transform.LookAt(target);
    }
}
