using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 startPos;

    public GameObject collisionEffect;
    private CameraFx camFx;

    public AudioClip[] collisionClips;
    public AudioClip paddleClip;
    public AudioClip failClip;
    private AudioSource mySource;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
        camFx = Camera.main.GetComponent<CameraFx>();
        mySource = GetComponent<AudioSource>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        AudioClip nextClip;
        if (collision.collider.name == "Paddle")
        {
            nextClip = paddleClip;
            camFx.StartScreenShake(false);
        } else
        {
            int thisClip = Mathf.FloorToInt(Random.Range(0, collisionClips.Length));
            nextClip = collisionClips[thisClip];
            camFx.StartScreenShake(true);
        }

        if (collision.transform.tag == "Brick")
        {
            GameObject particles = Instantiate(collisionEffect, collision.GetContact(0).point, Quaternion.identity);
            particles.transform.localScale = Vector3.one * .5f;

            ParticleSystem parts = particles.GetComponent<ParticleSystem>();
            float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
            Destroy(particles, totalDuration);
        }
        
        mySource.clip = nextClip;
        mySource.Play();
    }

    public void PlayFailSound()
    {
        mySource.clip = failClip;
        mySource.Play();
    }
}
