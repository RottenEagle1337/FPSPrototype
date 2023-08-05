using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [Header("Look settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float yRotationSpeed = 0.01f;
    [SerializeField] private float xCameraSpeed = 0.01f;
    [SerializeField] private float topAngleView = 90f;
    [SerializeField] private float bottomAngleView = -90f;

    [Header("UI")]
    [SerializeField] private Slider sensitivitySlider;

    private float wantedYRotation;
    private float currentYRotation;

    private float wantedCameraXRotation;
    private float currentCameraXRotation;

    private float rotationYVelocity;
    private float cameraXVelocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SetSensitivity();
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        wantedYRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        wantedCameraXRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        wantedCameraXRotation = Mathf.Clamp(wantedCameraXRotation, bottomAngleView, topAngleView);

        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, wantedCameraXRotation, ref cameraXVelocity, xCameraSpeed);

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(currentCameraXRotation, 0f, 0f);
    }

    public void SetWantedYRotation(float value)
    {
        wantedYRotation -= value;
    }

    public void SetWantedCameraXRotation(float value)
    {
        wantedCameraXRotation -= value;
    }

    public void SetSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
    }
}
