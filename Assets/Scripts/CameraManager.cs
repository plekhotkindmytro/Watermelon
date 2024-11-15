using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Follow(Transform target)
    {
       // cinemachineVirtualCamera.Follow = target;
    }
}
