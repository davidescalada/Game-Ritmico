using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasController : MonoBehaviour
{
    [SerializeField] float velocity;
    private Rigidbody2D rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * velocity * Time.deltaTime;
    }
    
    
}
