using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    public int jumpSpeed;
    public float moveForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(hor, 0) * moveForce);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity += Vector2.up * jumpSpeed;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            FindObjectOfType<GameManager>().Lose();
            DestroyPlayerIntoParticles();
        }
    }

    void DestroyPlayerIntoParticles()
    {
        // Bonus: Destroy the player into particles
        // You can use a particle system or instantiate particles manually
        // Here is a simple example using Instantiate for particles

        int numParticles = 10;
        for (int i = 0; i < numParticles; i++)
        {
            Vector3 particlePosition = transform.position + Random.onUnitSphere * 0.5f;
            GameObject particle = Instantiate(gameObject, particlePosition, Quaternion.identity);
            Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
            particleRb.velocity = Random.onUnitSphere * 2f;
            Destroy(particle, 2f);
        }

        Destroy(gameObject); // Destroy the original player
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Teleporter")
        {
            FindObjectOfType<GameManager>().Win();
        }
    }
}



