using UnityEngine;

public class RotacionPlataforma : MonoBehaviour
{
    private readonly float velocidadGradosPorSegundo = 2f;

    void Update()
    {
        transform.Rotate(0f, velocidadGradosPorSegundo * Time.fixedDeltaTime, 0f, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
