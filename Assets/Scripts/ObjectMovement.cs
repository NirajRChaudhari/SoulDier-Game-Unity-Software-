using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    [SerializeField] private GameObject[] borders;
    private int currentIndex = 0;
    [SerializeField] private float speed = 4f;

    private void Update()
    {
        // Object move between 2 waypoints logic
        if (Vector2.Distance(borders[currentIndex].transform.position, transform.position) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= borders.Length)
            {
                currentIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, borders[currentIndex].transform.position, Time.deltaTime * speed);
    }
}
