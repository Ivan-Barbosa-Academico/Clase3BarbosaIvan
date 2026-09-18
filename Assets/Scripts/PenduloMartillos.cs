using UnityEngine;

public class PenduloMartillos : MonoBehaviour
{
    public float swingAngle = 180f;

    public float swingSpeed = 0.2f;

    public Vector3 swingAxis = Vector3.forward;

    Quaternion initialLocalRotation;

    void Start()
    {
        initialLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Oscila usando seno para movimiento suave: valor en [-swingAngle/2, +swingAngle/2]
        float halfAngle = swingAngle * 0.5f;
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2f * swingSpeed) * halfAngle;

        Quaternion delta = Quaternion.AngleAxis(angle, swingAxis.normalized);
        transform.localRotation = initialLocalRotation * delta;
    }
}
