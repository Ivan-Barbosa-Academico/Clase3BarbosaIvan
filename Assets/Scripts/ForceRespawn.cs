using UnityEngine;

public class ForceRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Transform respawnPoint = RespawnManager.Instance != null ? RespawnManager.Instance.CurrentRespawnPoint : null;

        if (respawnPoint != null)
        {
            other.transform.position = respawnPoint.position;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            Debug.LogWarning("RespawnManager no tiene un punto de respawn configurado.");
        }
    }
}
