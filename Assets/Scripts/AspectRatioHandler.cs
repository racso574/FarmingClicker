using UnityEngine;

public class AspectRatioHandler : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f; // Cambia esta relación según lo necesites

    void Start()
    {
        // Esto solo es necesario para asegurarse de que la ventana comience con la relación de aspecto deseada
        SetAspectRatio(targetAspectRatio);
    }

    void Update()
    {
        // Esto asegura que la ventana mantenga la relación de aspecto al redimensionarse
        SetAspectRatio(targetAspectRatio);
    }

    void SetAspectRatio(float targetAspect)
    {
        int windowWidth = Screen.width;
        int windowHeight = Screen.height;

        // Calcular el nuevo tamaño basado en la relación de aspecto deseada
        float windowAspect = (float)windowWidth / (float)windowHeight;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Si el ancho es mayor que la relación de aspecto, ajustar la altura
            int newWidth = windowWidth;
            int newHeight = Mathf.RoundToInt(windowWidth / targetAspect);
            Rect rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
            Camera.main.rect = rect;
        }
        else
        {
            // Si la altura es mayor que la relación de aspecto, ajustar el ancho
            float scaleWidth = 1.0f / scaleHeight;
            int newWidth = Mathf.RoundToInt(windowHeight * targetAspect);
            int newHeight = windowHeight;
            Rect rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
            Camera.main.rect = rect;
        }
    }
}
