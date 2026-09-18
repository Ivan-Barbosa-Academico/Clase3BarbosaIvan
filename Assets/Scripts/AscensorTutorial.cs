using UnityEngine;

public class AscensorTutorial : MonoBehaviour
{
    public GameObject[] nodos;

    public float platformSpeed = 2f;

    int waypointIndex = 0;

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
}
