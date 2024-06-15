using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainedNoteController : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] float endTolerance; // Tolerancia en los últimos píxeles de la nota para considerar "Excelente"
    [SerializeField] float sustainedDuration; // Duración de la nota sostenida
    private Rigidbody2D rb;
    private bool isPressed;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;
    private float startTime;
    private bool isSustainedNoteActive;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalScale = transform.localScale;
        isSustainedNoteActive = false;
    }

    void FixedUpdate()
    {
         rb.velocity = Vector2.left * velocity * Time.deltaTime;
        // Cambiar de color si está presionada
        spriteRenderer.color = isPressed ? Color.yellow : originalColor;
        if (isSustainedNoteActive && Time.time - startTime >= sustainedDuration)
        {
            isSustainedNoteActive = false;
            DeleteNote(); 
        }
    }

    public void DeleteNote()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("ResetCombo"))
    //    {
    //        sceneController.ResetCombo();
    //        DeleteNote();
    //    }
    //}

    public void SetPressed(bool pressed)
    {
        isPressed = pressed;
        
    }

    public void StartSustainedNote()
    {
        startTime = Time.time;
        isSustainedNoteActive = true;
    }

    public bool IsNearEnd()
    {
        // Verificar si la nota está cerca del final basado en la tolerancia
        return transform.position.x <= originalScale.x * endTolerance;
    }
    public void SetWidth(float width)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = width;
        transform.localScale = newScale;
    }
}

