using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Camera camera;
    public float speed = 1f;
    public float gravity = 9.81f;
    public float groundedGravity = 0.1f;

    [Header("Salto")]
    public float jumpHeight = 1f;
    public float jumpBufferTime = 0.12f;

    [Header("Control horizontal")]
    public float groundAcceleration = 20f;
    public float airAcceleration = 8f;
    [Range(0f, 1f)]
    public float airControlFactor = 0.50f;

    private CharacterController controller;
    private float verticalVelocity = 0f;
    private Vector3 currentHorizontalVelocity = Vector3.zero;
    private float jumpBufferCounter = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    void FixedUpdate()
    {
        // --- INPUT como ejes X/Z (evita escala extra en diagonal) ---
        float inputX = 0f;
        float inputZ = 0f;
        if (Keyboard.current.aKey.isPressed) inputX -= 1f;
        if (Keyboard.current.dKey.isPressed) inputX += 1f;
        if (Keyboard.current.sKey.isPressed) inputZ -= 1f;
        if (Keyboard.current.wKey.isPressed) inputZ += 1f;

        Vector2 inputVec = new Vector2(inputX, inputZ);
        if (inputVec.sqrMagnitude > 1f) inputVec = inputVec.normalized;

        // Capturar pulsación de salto en buffer
        if (Keyboard.current.spaceKey.wasPressedThisFrame) jumpBufferCounter = jumpBufferTime;
        else jumpBufferCounter -= Time.fixedDeltaTime;

        // Convertir input 2D a dirección local 3D
        Vector3 moveDir = camera.transform.right * inputVec.x + camera.transform.forward * inputVec.y;
        transform.forward = moveDir;
        // Aplicar factor de control en aire
        float controlFactor = controller.isGrounded ? 1f : airControlFactor;
        Vector3 desiredHorizontal = moveDir * speed * controlFactor;

        // Aceleración distinta en suelo/aire
        float accel = controller.isGrounded ? groundAcceleration : airAcceleration;
        currentHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, desiredHorizontal, accel * Time.fixedDeltaTime);

        // Salto y gravedad
        if (controller.isGrounded)
        {
            if (jumpBufferCounter > 0f)
            {
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
                jumpBufferCounter = 0f;
            }
            else
            {
                verticalVelocity = -groundedGravity;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.fixedDeltaTime;
        }

        Vector3 velocity = currentHorizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.fixedDeltaTime);
    }
}
