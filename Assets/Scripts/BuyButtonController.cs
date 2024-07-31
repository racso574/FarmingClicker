using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButtonController : MonoBehaviour
{
    public long baseCost = 20; // Costo inicial del botón
    public long precioBoton; // Precio actual del botón
    public float factorIncremento = 1.15f; // Factor de incremento para el precio
    private int compraNumero = 0; // Contador de compras realizadas
    public Sprite spriteActivo; // Sprite para el estado activo
    public Sprite spriteInactivo; // Sprite para el estado inactivo
    public Image botonImage; // Imagen del botón
    public TMP_Text textoPrecio; // Referencia al TextMeshPro del botón
    public Button boton; // Referencia al componente Button de Unity

    public GameObject prefabToInstantiate; // Prefab que se va a instanciar
    public Transform instantiationPoint; // Punto de instanciación

    void Start()
    {
        // Inicializar el precio del botón
        precioBoton = baseCost;
        textoPrecio.text = precioBoton.ToString();
    }

    void Update()
    {
        // Obtener los puntos actuales del jugador
        long puntosActuales = PointsController.Instance.GetPoints();

        // Verificar si los puntos actuales son suficientes para el precio del botón
        if (puntosActuales >= precioBoton)
        {
            // Cambiar el sprite del botón al sprite activo y habilitar el botón
            botonImage.sprite = spriteActivo;
            boton.interactable = true;
        }
        else
        {
            // Cambiar el sprite del botón al sprite inactivo y deshabilitar el botón
            botonImage.sprite = spriteInactivo;
            boton.interactable = false;
        }
    }

    public void OnButtonClick()
    {
        if (PointsController.Instance.GetPoints() >= precioBoton)
        {
            // Restar los puntos del jugador
            PointsController.Instance.SubtractPoints(precioBoton);

            // Incrementar el contador de compras
            compraNumero++;

            // Calcular el nuevo precio usando una fórmula exponencial con factor editable
            precioBoton = (long)(baseCost * Mathf.Pow(factorIncremento, compraNumero));

            // Actualizar el texto del precio del botón
            textoPrecio.text = precioBoton.ToString();

            // Instanciar el upgrade
            InstanciateUpgrade();
        }
    }

    private void InstanciateUpgrade()
    {
        if (prefabToInstantiate != null && instantiationPoint != null)
        {
            // Instanciar el prefab en la posición y rotación del punto de instanciación
            Instantiate(prefabToInstantiate, instantiationPoint.position, instantiationPoint.rotation);
        }
    }
}


