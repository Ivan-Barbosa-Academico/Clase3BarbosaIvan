using UnityEngine;

public class OnCollisionRotativos : MonoBehaviour
{
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

            float scale = 0.29289f;
            collision.gameObject.transform.localScale = new Vector3(scale, scale, scale); // Si. Es una solución horrible y chapucera, pero si me detenía a buscar una mejor no hubiera llegado a hacer el otro proyecto.
        }
    }
}
