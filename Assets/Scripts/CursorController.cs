using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CursorController : MonoBehaviour
{
    // Offset para ajustar la posición del objeto relativo al cursor
    public Vector3 cursorOffset;

    [SerializeField]
    private float rotationAngle = 15f; // Ángulo de rotación en grados

    [SerializeField]
    private float rotationDuration = 0.2f; // Duración de la rotación en segundos

    public UnityEvent audioTrigger; // UnityEvent para activar eventos de audio

    private Coroutine rotationCoroutine;
    private Quaternion initialRotation;

    void Start()
    {
        // Ocultar completamente el cursor del sistema
        Cursor.visible = false;

        // Guardar la rotación inicial del objeto
        initialRotation = transform.rotation;
    }

    void Update()
    {
        Cursor.visible = false;
        Vector3 mousePosition = Input.mousePosition;

        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Ajustar la posición Z del cursor para asegurarse de que esté en el plano visible
        worldPosition.z = 0;

        // Aplicar el offset a la posición del cursor
        worldPosition += cursorOffset;

        // Mover el objeto a la posición del mouse con el offset aplicado
        transform.position = worldPosition;

        // Detectar clic del ratón
        if (Input.GetMouseButtonDown(0))
        {
            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
                // Asegurarse de que el objeto vuelve a la rotación inicial
                transform.rotation = initialRotation;
            }
            rotationCoroutine = StartCoroutine(RotateCursor());

            // Invocar el UnityEvent cuando el cursor rota
            audioTrigger?.Invoke();
        }
    }

    private IEnumerator RotateCursor()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

        // Rotar hacia la derecha
        float elapsedTime = 0;
        while (elapsedTime < rotationDuration / 2)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / (rotationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

        // Rotar de vuelta a la posición original
        elapsedTime = 0;
        while (elapsedTime < rotationDuration / 2)
        {
            transform.rotation = Quaternion.Lerp(targetRotation, initialRotation, elapsedTime / (rotationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = initialRotation;

        rotationCoroutine = null;
    }
}
