using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlataformasAgarre : MonoBehaviour
{
    public GameObject[] nodos;

    public float platformSpeed = 2f;

    int waypointIndex = 0;

    // Guarda restricciones originales por GameObject para restaurarlas
    private Dictionary<GameObject, RigidbodyConstraints> savedConstraints = new();

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if(Vector3.Distance(transform.position, nodos[waypointIndex].transform.position) < 0.1f) //esto compara la distancia entre la plataforma y el punto al que se dirije. Si detecta que ya llegó, va al otro nodo. Es como cuando ordenaba variables con metodo de cascada en C++
        {
            waypointIndex++;
            if(waypointIndex >= nodos.Length)
            {
                waypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nodos[waypointIndex].transform.position, platformSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Keyboard.current.eKey.isPressed)
            {
                // Anidar al jugador y congelar su eje Y (guardando restricciones originales)
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (!savedConstraints.ContainsKey(collision.gameObject))
                        savedConstraints[collision.gameObject] = rb.constraints;
                    rb.constraints |= RigidbodyConstraints.FreezePositionY;
                }
                collision.gameObject.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Keyboard.current.eKey.isPressed)
            {
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (!savedConstraints.ContainsKey(collision.gameObject))
                        savedConstraints[collision.gameObject] = rb.constraints;
                    rb.constraints |= RigidbodyConstraints.FreezePositionY;
                }
                    collision.gameObject.transform.SetParent(transform);
                
            }
            else
            {
                // Si ya no pulsa E, restaurar restricciones y quitar parent
                if (savedConstraints.TryGetValue(collision.gameObject, out var original))
                {
                    var rb = collision.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.constraints = original;
                    savedConstraints.Remove(collision.gameObject);
                }
                // Aseguramos que el jugador deje de ser hijo de la plataforma
                if (collision.gameObject.transform.parent == transform)
                    collision.gameObject.transform.SetParent(null);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restaurar restricciones si quedan guardadas
            if (savedConstraints.TryGetValue(collision.gameObject, out var original))
            {
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.constraints = original;
                savedConstraints.Remove(collision.gameObject);
            }
            // Quitar parent al salir de la colisión
            if (collision.gameObject.transform.parent == transform)
                collision.gameObject.transform.SetParent(null);

            float scale = 0.29289f;
            collision.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
