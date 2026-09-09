using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;              // el personaje (eje central)
    public float targetHeight = 1.5f;     // altura relativa al target donde mirar
    public float distance = 3f;           // distancia al target
    public float sensitivity = 150f;      // sensibilidad del ratón
    public float minPitch = -30f;         // tope inferior de la cámara
    public float maxPitch = 60f;          // tope superior de la cámara
    public bool lockCursor = true;        // bloquear cursor al jugar
    public bool invertY = false;

    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (target == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null) target = player.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Leer movimiento del ratón (Input System)
        Vector2 delta = Vector2.zero;
        if (Mouse.current != null)
            delta = Mouse.current.delta.ReadValue();

        // Actualizar ángulos
        yaw += delta.x * sensitivity * Time.deltaTime;
        pitch += (invertY ? 1 : -1) * delta.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calcular posición y orientación
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPos = target.position + Vector3.up * targetHeight;
        Vector3 desiredPos = targetPos + rot * new Vector3(0f, 0f, -distance);

        // Colocar cámara y mirar al target
        transform.position = desiredPos;
        transform.rotation = Quaternion.LookRotation((targetPos - transform.position).normalized, Vector3.up);
    }
}