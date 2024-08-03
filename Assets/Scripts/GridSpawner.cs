using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // El GameObject que quieres instanciar
    public Vector2 startPosition; // La posición inicial para empezar a instanciar
    public Vector2 offset; // El desplazamiento entre cada objeto instanciado
    public int rows; // Número de filas en la cuadrícula
    public int columns; // Número de columnas en la cuadrícula

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Lista para almacenar los objetos instanciados

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

                // Configuramos el Order in Layer del SpriteRenderer según la fila
                SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = i + 1; // El Order in Layer se incrementa con cada fila
                }

                // Añadimos el objeto a la lista
                spawnedObjects.Add(spawnedObject);
            }
        }
    }

    // Función para reducir el delay en todos los WheatControllers
    public void ReduceGrowthDelay()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            WheatController wheatController = obj.GetComponent<WheatController>();
            if (wheatController != null)
            {
                wheatController.ReduceDelay();
            }
        }
    }
}
