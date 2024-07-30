using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointsText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        UpdatePointsText(); // Update the text when the game starts
    }

    private void OnEnable()
    {
        // Subscribe to the events from PointsController
        PointsController.OnPointsChanged += UpdatePointsText;
    }

    private void OnDisable()
    {
        // Unsubscribe from the events to avoid memory leaks
        PointsController.OnPointsChanged -= UpdatePointsText;
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = "" + PointsController.Instance.GetPoints().ToString();
        }
    }
}
