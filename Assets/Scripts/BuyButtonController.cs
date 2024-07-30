using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButtonController : MonoBehaviour
{
    public long precioBoton; // Precio del botón
    public Sprite spriteActivo; // Sprite para el estado activo
    public Sprite spriteInactivo; // Sprite para el estado inactivo
    public Image botonImage; // Imagen del botón
    public TMP_Text textoPrecio; // Referencia al TextMeshPro del botón

    void Start()
    {
        // Asignar el precio al texto del botón al inicio
        textoPrecio.text = precioBoton.ToString();
    }

    void Update()
    {
        // Obtener los puntos actuales del jugador
        long puntosActuales = PointsController.Instance.GetPoints();

        // Verificar si los puntos actuales son suficientes para el precio del botón
        if (puntosActuales >= precioBoton)
        {
            // Cambiar el sprite del botón al sprite activo
            botonImage.sprite = spriteActivo;
        }
        else
        {
            // Cambiar el sprite del botón al sprite inactivo
            botonImage.sprite = spriteInactivo;
        }
    }

    public void OnButtonClic(){
        PointsController.Instance.SubtractPoints(precioBoton);
    }
}
