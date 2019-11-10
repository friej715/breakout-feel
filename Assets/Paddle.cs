using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Vector3 startPosition;
    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition() {
        float x = Input.GetAxis("Horizontal");
		transform.position = new Vector3(transform.position.x + (x * speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    void Reset() {
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        if (collision.transform.name == "Ball")
        {
            Vector3 localPosition = transform.InverseTransformPoint(collision.GetContact(0).point);
            float newForce = (localPosition.x * 10) + Mathf.Abs(Input.GetAxis("Horizontal")) * 40;
            collision.collider.GetComponent<Rigidbody2D>().AddForce(new Vector3(newForce, 0, 0));
        }
    }

}
