using UnityEngine;

public class RotacionPlataforma : MonoBehaviour
{
    private readonly float velocidadGradosPorSegundo = 2f;

    void Update()
    {
        transform.Rotate(0f, velocidadGradosPorSegundo * Time.fixedDeltaTime, 0f, Space.Self);
    }
}
