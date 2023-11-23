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
        isGrounded = true;

        if (collision.gameObject.CompareTag("enemy"))
        {
            FindObjectOfType<GameManager>().Lose();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
        }
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Teleporter")
        {
            FindObjectOfType<GameManager>().Win();
        }

        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
    }
}



