using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Transform middleBackground, farBackgroud;

    public float minHeight, maxHeight;

    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.position is by default points to the object to which current script is linked i.e, MainCamera
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y + 3, minHeight, maxHeight), transform.position.z);


        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        lastPos = transform.position;

        // Keep z same just increment x and y position by 0.5 * amountToMoveX
        //middleBackground.position = new Vector3(middleBackground.position.x + (amountToMove.x * 0.5f), middleBackground.position.y + (amountToMove.y * 0.5f), middleBackground.position.z);

        // Keep z same just increment x and y position by amountToMoveX
        //farBackgroud.position = new Vector3(farBackgroud.position.x + amountToMove.x, farBackgroud.position.y + amountToMove.y, farBackgroud.position.z);
    }
}
