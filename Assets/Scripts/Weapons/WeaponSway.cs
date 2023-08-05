using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float smoothness = 8f;
    [SerializeField] private float swayMultiplier = 2f;

    private void Update()
    {
        HandleSway();
    }

    private void HandleSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotationX * rotationY, smoothness * Time.deltaTime);
    }
}
