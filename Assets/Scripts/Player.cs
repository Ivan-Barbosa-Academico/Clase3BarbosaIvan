using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Transform target;
    public Camera camera;
    public float speed = 5f;
    public float gravity = 9.81f;
    public float groundedGravity = 0.1f;

    [Header("Salto")]
    public float jumpHeight = 2f; // altura objetivo del salto en metros

    private CharacterController controller;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;

        if (target == null)
        {
            var player = GameObject.FindWithTag("Amogus");
            if (player != null) target = player.transform;
        }
    }

    void Update()
    {
        // --- INPUT horizontal (ejes X/Z) ---
        float inputX = 0f;
        float inputZ = 0f;
        if (Keyboard.current.aKey.isPressed) inputX -= 1f;
        if (Keyboard.current.dKey.isPressed) inputX += 1f;
        if (Keyboard.current.sKey.isPressed) inputZ -= 1f;
        if (Keyboard.current.wKey.isPressed) inputZ += 1f;

        Vector2 inputVec = new Vector2(inputX, inputZ);
        if (inputVec.sqrMagnitude > 1f) inputVec = inputVec.normalized;

        // Dirección relativa a la cámara (o al transform si no hay cámara)
        Vector3 moveDir = Vector3.zero;
        if (camera != null)
            moveDir = camera.transform.right * inputVec.x + camera.transform.forward * inputVec.y;
        else
            moveDir = transform.right * inputVec.x + transform.forward * inputVec.y;
        moveDir.y = 0f;

        Vector3 horizontal = moveDir * speed;

        if (target != null)
        {
            // Rotar el target hacia la dirección de movimiento
            target.transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);
    
        }
        // --- SALTO simple: solo si está grounded y se pulsa Space ---
        if (controller.isGrounded)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
            else
            {
                // pequeña fuerza hacia abajo para mantener contacto con el suelo
                // verticalVelocity = -groundedGravity;
            }
        }
        else
        {
            // en el aire aplicar gravedad
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 velocity = horizontal + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
