using Unity.VisualScripting;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Victoria!");
        }
    }
}
