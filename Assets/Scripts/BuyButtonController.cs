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
    public Sprite sprite1; // Sprite para el estado activo e inactivo después de la primera compra
    public Sprite sprite2; // Sprite para el estado inicial antes de la primera compra
    public Image botonImage; // Imagen del botón
    public TMP_Text textoPrecio; // Referencia al TextMeshPro del botón
    public Button boton; // Referencia al componente Button de Unity

    public GameObject prefabToInstantiate; // Prefab que se va a instanciar
    public Transform instantiationPoint; // Punto de instanciación

    public BuyButtonController botonReferencia; // Referencia a otro botón

    private Color colorInactivo;
    private bool nuncaComprable = true; // Indica si el botón nunca ha sido presionado
    private bool referenciaComprada = false; // Indica si el botón de referencia ha sido comprado

    void Start()
    {
        // Inicializar el precio del botón
        precioBoton = baseCost;
        textoPrecio.text = precioBoton.ToString();

        // Definir el color inactivo usando el valor hexadecimal exacto
        ColorUtility.TryParseHtmlString("#B0B0B0", out colorInactivo);

        // Configurar el estado inicial del botón
        if (botonReferencia == null)
        {
            SetEstado(3); // Sin referencia, inicia en estado 3
            referenciaComprada = true;

        }
        else
        {
            SetEstado(4); // Con referencia, inicia en estado 4
        }
    }

    void Update()
    {
        // Obtener los puntos actuales del jugador
        long puntosActuales = PointsController.Instance.GetPoints();

        if (botonReferencia != null && botonReferencia.compraNumero > 0 && referenciaComprada == false )
        {
            referenciaComprada = true;
            SetEstado(3);
        }

        if (referenciaComprada == true){
            if (puntosActuales >= precioBoton)
            {
                SetEstado(1); // Estado 1 si se puede comprar
                nuncaComprable = false;
            }
            else
            {
                if (nuncaComprable)
                {
                    SetEstado(3);
                }else{
                    SetEstado(2);
                }
                
            }
            
            
        }

        
    }

    private void SetEstado(int estado)
    {
        switch (estado)
        {
            case 1:
                botonImage.sprite = sprite1;
                botonImage.color = new Color(1f, 1f, 1f, 1f); // Color normal
                textoPrecio.text = precioBoton.ToString();
                boton.interactable = true;
                break;
            case 2:
                botonImage.sprite = sprite1;
                botonImage.color = colorInactivo; // Color apagado
                textoPrecio.text = precioBoton.ToString();
                boton.interactable = false;
                break;
            case 3:
                botonImage.sprite = sprite2;
                botonImage.color = colorInactivo; // Color apagado
                textoPrecio.text = precioBoton.ToString();
                boton.interactable = false;
                break;
            case 4:
                botonImage.sprite = sprite2;
                botonImage.color = colorInactivo; // Color apagado
                textoPrecio.text = "???";
                boton.interactable = false;
                break;
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

            // Instanciar la mejora (suponiendo que debería suceder al comprar)
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
