using System;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    // Singleton instance
    public static PointsController Instance { get; private set; }

    // A variable to store the points
    public long points;

    // Event to notify listeners about points changes
    public static event Action OnPointsChanged;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure that there's only one instance of PointsController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the object alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to add 1 to the points
    public void AddPoint()
    {
        points++;
        OnPointsChanged?.Invoke(); // Notify listeners
    }

    // Function to subtract x points
    public void SubtractPoints(long x)
    {
        points -= x;
        if (points < 0)
        {
            points = 0; // Ensure points don't go negative
        }
        OnPointsChanged?.Invoke(); // Notify listeners
    }

    // Function to get the current points
    public long GetPoints()
    {
        return points;
    }
}
