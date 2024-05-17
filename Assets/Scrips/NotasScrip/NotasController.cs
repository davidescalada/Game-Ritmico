using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasController : MonoBehaviour
{
    [SerializeField] float velocity;
    private Rigidbody2D rb;
    public SceneController sceneController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
