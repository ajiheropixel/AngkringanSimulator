using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;

    public float distance = 6f;
    public float height = 3f;

    public float mouseSensitivity = 3f;

    private float yaw = 0f;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * mouseSensitivity * 0.1f;
            pitch -= mouseDelta.y * mouseSensitivity * 0.1f;

            pitch = Mathf.Clamp(pitch, -20f, 60f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition = player.position + Vector3.up * 1f;

        Vector3 cameraOffset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = targetPosition + cameraOffset;

        transform.LookAt(targetPosition);
    }
}