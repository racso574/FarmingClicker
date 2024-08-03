using System.Collections;
using UnityEngine;

public class WheatController : MonoBehaviour
{
    // Growth states
    public int currentState;
    public Sprite[] growthSprites; // Array to hold sprites for states 1, 2, and 3
    public GameObject juicyClickPrefab; // Prefab for the juicy click animation

    private SpriteRenderer spriteRenderer;
    public float currentDelay = 10f; // El delay actual, inicializado en 10 segundos
    private const float minDelay = 4f; // El delay mínimo al que puede llegar
    private float delayRest = 2f;
    void Start()
    {
        currentState = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Grow());
    }

    private void UpdateSprite()
    {
        if (currentState == 0)
        {
            spriteRenderer.sprite = null; // No sprite in state 0
        }
        else if (currentState > 0 && currentState <= 3)
        {
            spriteRenderer.sprite = growthSprites[currentState - 1]; // Assign appropriate sprite
        }
    }

    private IEnumerator Grow()
    {
        while (true)
        {
            if (currentState < 3)
            {
                float delay = Random.Range(1f, currentDelay); // Random delay between 1 and currentDelay seconds
                yield return new WaitForSeconds(delay);
                currentState++;
                UpdateSprite();
            }
            else
            {
                yield return null; // Stop growing at state 3
            }
        }
    }

    void OnMouseDown()
    {
        if (currentState == 3)
        {
            Harvest();
        }
    }

    public void Harvest()
    {
        // Harvesting logic
        PointsController.Instance?.AddPoint();
        currentState = 0;
        UpdateSprite();
        StopAllCoroutines(); // Stop the current growth coroutine
        StartCoroutine(Grow()); // Start the growth process again

        // Instanciar la animación
        Instantiate(juicyClickPrefab, transform.position, Quaternion.identity);
    }

    public void ReduceDelay()
    {
        currentDelay = Mathf.Max(minDelay, currentDelay - delayRest); // Reducir el delay sin bajar de minDelay
    }
}
