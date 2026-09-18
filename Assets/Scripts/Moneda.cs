using UnityEngine;

public class Moneda : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Agarraste la moneda");
            Destroy(gameObject);
        }
    }
}
