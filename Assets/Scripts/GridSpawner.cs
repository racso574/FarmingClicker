using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // El GameObject que quieres instanciar
    public Vector2 startPosition; // La posición inicial para empezar a instanciar
    public Vector2 offset; // El desplazamiento entre cada objeto instanciado
    public int rows; // Número de filas en la cuadrícula
    public int columns; // Número de columnas en la cuadrícula

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Calculamos la posición para instanciar cada objeto
                Vector2 position = new Vector2(startPosition.x + j * offset.x, startPosition.y - i * offset.y);
                
                // Instanciamos el objeto en la posición calculada y lo asignamos como hijo del objeto que contiene este script
                GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);
                spawnedObject.transform.SetParent(transform);
            }
        }
    }
}
