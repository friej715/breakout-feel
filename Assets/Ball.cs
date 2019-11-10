using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 startPos;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
    }

    public void Reset()
    {
        transform.position = startPos;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        
    }

    public void StartMoving()
    {
        rb.AddForce(Vector3.down * 300);
    }
}
