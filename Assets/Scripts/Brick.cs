using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    bool shouldFadeOut;
    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        shouldFadeOut = false;
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeOut == true)
        {
            Color curColor = myRenderer.material.color;
            curColor.a -= .01f;
            myRenderer.material.color = curColor;
            if (curColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.transform.name == "Ball")
        {

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.AddTorque(Random.Range(-360, 360));
            myRenderer.color = Color.yellow;

            Destroy(gameObject.GetComponent<BoxCollider2D>());
            shouldFadeOut = true;
        }
    }
}
