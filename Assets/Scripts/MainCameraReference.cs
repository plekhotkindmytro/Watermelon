using UnityEngine;

public class MainCameraReference : MonoBehaviour
{
    // Static property to access the main camera
    public static Camera MainCamera { get; private set; }

    // Ensure the main camera is set once when the script is initialized
    private void Awake()
    {
        // Check if the main camera is already set
        if (MainCamera == null)
        {
            // Assign this camera as the main camera
            MainCamera = GetComponent<Camera>();
        }
        else if (MainCamera != GetComponent<Camera>())
        {
            Debug.LogWarning("Multiple MainCameraReference instances detected! This script should only be on one camera.");
        }
    }

    private void OnDestroy()
    {
        // Clear the static reference if this camera is destroyed
        if (MainCamera == GetComponent<Camera>())
        {
            MainCamera = null;
        }
    }
}
