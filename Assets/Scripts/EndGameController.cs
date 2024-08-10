using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    private bool isEndReady = false;
    private bool endPlayed = false;

    public GameObject panel;
    public float moveSpeed = 5f;
    private Vector3 originalPosition;

    void Start()
    {
        // Guardar la posición original del panel
        originalPosition = panel.transform.position;

        // Suscribirse al evento de cambio de puntos para monitorear el progreso
        PointsController.OnPointsChanged += CheckWinCondition;
        endPlayed = false;
        isEndReady = false;
    }

    public void SetEndReady()
    {
        Debug.Log("aaa");
        isEndReady = true;
    }

    private void CheckWinCondition()
    {
        long currentPoints = PointsController.Instance.GetPoints();

        if (currentPoints >= 5000 && isEndReady && !endPlayed)
        {
            Debug.Log("¡Felicidades, ganaste!");
            endPlayed = true;

            // Iniciar la corrutina para mover el panel hacia abajo
            StartCoroutine(MovePanelDown());
        }
    }

    private IEnumerator MovePanelDown()
    {
        Vector3 targetPosition = new Vector3(panel.transform.position.x, 0, panel.transform.position.z);

        while (Vector3.Distance(panel.transform.position, targetPosition) > 0.1f)
        {
            panel.transform.position = Vector3.MoveTowards(panel.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Asegurarse de que el panel esté exactamente en la posición de destino
        panel.transform.position = targetPosition;
    }

    public void MoveUpPanel()
    {
        StartCoroutine(MovePanelUp());
    }

    private IEnumerator MovePanelUp()
    {
        Vector3 targetPosition = originalPosition;

        while (Vector3.Distance(panel.transform.position, targetPosition) > 0.1f)
        {
            panel.transform.position = Vector3.MoveTowards(panel.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Asegurarse de que el panel esté exactamente en la posición original
        panel.transform.position = targetPosition;
    }

    private void OnDestroy()
    {
        PointsController.OnPointsChanged -= CheckWinCondition;
    }
}
