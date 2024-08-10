using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFarmingblade : MonoBehaviour
{
    // Parámetro de velocidad de rotación (grados por segundo)
    public float rotationSpeed = 100f;

    private void Update()
    {
        // Rotar el objeto sobre su eje Z a la velocidad especificada
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Verifica si el objeto con el que colisiona tiene el script WheatController
        WheatController wheat = other.GetComponent<WheatController>();

        if (wheat != null && wheat.currentState == 3)
        {
            wheat.Harvest();
        }
    }
}
