using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Vector3 startPosition;
    private float speed = 5;

    private AudioSource mySource;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        mySource = GetComponent<AudioSource>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition() {
        float x = Input.GetAxis("Horizontal");
        mySource.volume = Mathf.Abs(x);
        mySource.pitch = Remap(Mathf.Abs(x), 0, 1, 1, 3);
        transform.position = new Vector3(transform.position.x + (x * speed * Time.deltaTime), transform.position.y, transform.position.z);

        Vector3 temporaryScale = transform.localScale;
        temporaryScale.y = Remap(Mathf.Abs(x), 0, 1, 1, .75f);
        transform.localScale = temporaryScale;
    }

    void Reset() {
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Ball")
        {
            Vector3 localPosition = transform.InverseTransformPoint(collision.GetContact(0).point);
            float newForce = (localPosition.x*50) + Mathf.Abs(Input.GetAxis("Horizontal")*-50);
            collision.collider.GetComponent<Rigidbody2D>().AddForce(new Vector3(newForce, 0, 0));
        }
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
