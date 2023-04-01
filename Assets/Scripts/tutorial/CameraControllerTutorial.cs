using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTutorial : MonoBehaviour
{

    public Transform target;
    public Transform middleBackground, farBackgroud;

    public float minHeight, maxHeight;

    private Vector2 lastPos;

    private Camera cam;
    private float targetZoomOut;
    private float targetZoomReset;
    private float zoomLerpSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;

        cam = Camera.main;
        targetZoomOut = cam.orthographicSize + 8f;
        targetZoomReset = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.position is by default points to the object to which current script is linked i.e, MainCamera
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y + 2, minHeight, maxHeight), transform.position.z);


        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        lastPos = transform.position;

        // Keep z same just increment x and y position by 0.5 * amountToMoveX
        //middleBackground.position = new Vector3(middleBackground.position.x + (amountToMove.x * 0.5f), middleBackground.position.y + (amountToMove.y * 0.5f), middleBackground.position.z);

        // Keep z same just increment x and y position by amountToMoveX
        //farBackgroud.position = new Vector3(farBackgroud.position.x + amountToMove.x, farBackgroud.position.y + amountToMove.y, farBackgroud.position.z);

        if (Input.GetButton("Zoom"))
        {
            Debug.Log("Zoooooooom Out");
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoomOut, Time.deltaTime * zoomLerpSpeed);
        }

        if (Input.GetButtonUp("Zoom"))
        {
            Debug.Log("Zoooooooom In");
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoomIn, Time.deltaTime * zoomLerpSpeed);
            cam.orthographicSize = targetZoomReset;
        }
    }
}
