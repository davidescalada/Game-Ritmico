using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasController : MonoBehaviour
{
    [SerializeField] float velocity;
    private Rigidbody2D rb;
    private SceneController sceneController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sceneController = FindObjectOfType<SceneController>();
    }

   
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * velocity * Time.deltaTime;
    }

    public void DeleteNote()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetCombo"))
        {
             Debug.Log("RESETO EL COMBO");
             sceneController.ResetCombo();
        }
    }
}
