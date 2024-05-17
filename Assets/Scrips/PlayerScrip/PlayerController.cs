using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 upperPosition;
    [SerializeField] Vector2 lowerPosition;
    [SerializeField] Vector2 centerPosition ;
    [SerializeField] float speed;
    private Vector2 targetPosition;
    private Rigidbody2D rb;
    public static event Action OnNoteCollided;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = centerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        PosicionamientoInputs();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Notas"))
        {
            Debug.Log("Player colisiono con una nota");
            OnNoteCollided?.Invoke();
        }
    }

    private void PosicionamientoInputs()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            targetPosition = upperPosition;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition = lowerPosition;
        }
        else
        {
            targetPosition = centerPosition;
        }
    }

}
