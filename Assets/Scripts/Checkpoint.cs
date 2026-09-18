using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (RespawnManager.Instance != null)
        {
            RespawnManager.Instance.SetCheckpoint(transform, gameObject);
            // Opcional: cambiar apariencia para indicar activación
        }
    }
}