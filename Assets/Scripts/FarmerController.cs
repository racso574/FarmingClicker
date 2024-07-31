using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : MonoBehaviour
{
    public float moveDistance = 5f; // Distancia que recorrerá el granjero en cada línea
    public float speed = 2f; // Velocidad de movimiento
    public float lineHeight = 1f; // Distancia que el granjero baja cuando cambia de línea
    public int maxLines = 18; // Número máximo de líneas antes de reiniciar
    private int direction = 1; // 1 para derecha, -1 para izquierda
    private Vector2 startPosition;
    private Vector2 originalStartPosition; // Nueva variable para la posición inicial original
    private int lineCount = 0; // Contador de líneas

    private void Start()
    {
        startPosition = transform.position;
        originalStartPosition = startPosition; // Guardamos la posición inicial original
    }

    private void Update()
    {
        MoveFarmer();
    }

    private void MoveFarmer()
    {
        float moveStep = speed * Time.deltaTime * direction;
        transform.Translate(moveStep, 0, 0);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            // Cambia la dirección y rota el personaje
            direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

            // Avanza a la siguiente línea
            transform.position = new Vector2(transform.position.x, transform.position.y - lineHeight); // Baja una línea
            startPosition = new Vector2(transform.position.x, startPosition.y - lineHeight); // Actualiza la posición de inicio para la nueva línea
            lineCount++;

            // Debug log para verificar el conteo de líneas
            Debug.Log("Line count: " + lineCount);

            // Reinicia la posición después de alcanzar el número máximo de líneas
            if (lineCount >= maxLines)
            {
                transform.position = originalStartPosition; // Reiniciamos a la posición inicial original
                startPosition = originalStartPosition; // Restablecemos el startPosition
                direction = 1; // Aseguramos que la dirección sea hacia la derecha
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Asegura que la escala esté orientada correctamente
                lineCount = 0;

                // Debug log para confirmar el reinicio
                Debug.Log("Resetting position and direction after reaching max lines.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug log para detección de colisiones
        Debug.Log("Collided with: " + other);

        // Verifica si el objeto con el que colisiona tiene el script WheatController
        WheatController wheat = other.GetComponent<WheatController>();

        if (wheat != null && wheat.currentState == 3)
        {
            wheat.Harvest();
        }
    }
}
