using System.Collections;
using UnityEngine;

public class JuicyClick : MonoBehaviour
{
    public float floatUpDistance = 1.0f; // Distancia hacia arriba
    public float floatRightDistance = 0.5f; // Distancia hacia la derecha
    public float fallDistance = 2.0f; // Distancia hacia abajo
    public float duration = 1.0f; // Duración de la animación

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(Animate());
    }

 private IEnumerator Animate()
{
    float elapsedTime = 0f;
    float extendedDuration = duration * 1.5f; // Extiende la duración de la animación
    Vector3 initialVelocity = new Vector3(floatRightDistance, floatUpDistance * 1.5f, 0) / duration; // Incrementa la fuerza hacia arriba
    Vector3 gravity = new Vector3(0, -fallDistance * 2, 0) / (duration * duration); // Gravedad
    float rotationSpeed = 90f / extendedDuration; // Velocidad de rotación hacia la derecha (grados por segundo)

    while (elapsedTime < extendedDuration)
    {
        // Mover el objeto basado en la velocidad y la gravedad
        transform.position += initialVelocity * Time.deltaTime;
        initialVelocity += gravity * Time.deltaTime; // La gravedad reduce la velocidad vertical

        // Aplicar rotación
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        elapsedTime += Time.deltaTime;
        yield return null;
    }

    Destroy(gameObject); // Destruir el objeto después de la animación
}


}
