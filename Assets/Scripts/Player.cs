using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hola");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Adios");
        if (Keyboard.current.wKey.IsPressed())
        {
            transform.position += Vector3.forward*speed*Time.deltaTime;
        }
        if (Keyboard.current.sKey.IsPressed())
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Keyboard.current.aKey.IsPressed())
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Keyboard.current.dKey.IsPressed())
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
