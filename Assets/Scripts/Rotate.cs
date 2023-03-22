using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private float speed = 14f;
    void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime * speed);
    }
}
