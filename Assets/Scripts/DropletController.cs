using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletController : MonoBehaviour
{
    private BoxCollider2D collider;
    [SerializeField] private LayerMask jumpableGround;
    private float startX, startY;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        collider = GetComponent<BoxCollider2D>();
        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            //Debug.Log("Drop on ground");
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector2(startX, startY);
            gameObject.SetActive(true);
        }

    }

    private bool isGrounded()
    {
        //BoxCast method returns true if player box overlaps with ground layer.
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
