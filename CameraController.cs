using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Configuración de Cámaras")]
    public Camera[] cameras;
    public KeyCode switchKey = KeyCode.C;

    private int currentCameraIndex = 0;

    void Start()
    {
        if (cameras == null || cameras.Length == 0)
        {
            Debug.LogError("No se han asignado cámaras");
            return;
        }

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            UpdateCameras();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && cameras.Length >= 1)
        {
            currentCameraIndex = 0;
            UpdateCameras();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && cameras.Length >= 2)
        {
            currentCameraIndex = 1;
            UpdateCameras();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && cameras.Length >= 3)
        {
            currentCameraIndex = 2;
            UpdateCameras();
        }
    }

    private void UpdateCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == currentCameraIndex);
        }
        
        Debug.Log("Cámara activa: " + currentCameraIndex);
    }
}
