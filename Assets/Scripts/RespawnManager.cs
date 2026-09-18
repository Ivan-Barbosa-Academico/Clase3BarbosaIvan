using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    [Tooltip("Punto de respawn actual.")]
    public Transform CurrentRespawnPoint { get; private set; }

    private GameObject currentCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Si quieres que el manager persista entre escenas:
        // DontDestroyOnLoad(gameObject);
    }

    public void SetCheckpoint(Transform point, GameObject checkpoint)
    {
        if (currentCheckpoint != null && currentCheckpoint != checkpoint)
        {
            Destroy(currentCheckpoint);
        }

        currentCheckpoint = checkpoint;
        CurrentRespawnPoint = point;
        // Aquí puedes añadir efectos/sonidos si lo deseas
    }
}