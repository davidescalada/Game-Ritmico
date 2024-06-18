using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainedNoteController : MonoBehaviour
{
    private SceneController sceneController;
    [SerializeField] float velocity;
    [SerializeField] float endTolerance; // Tolerancia en los últimos píxeles de la nota para considerar "Excelente"
 
    private Rigidbody2D rb;
    private bool isPressed;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float startTime;
    //private bool isSustainedNoteActive;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        //isSustainedNoteActive = false;
        startTime = Time.time;
    }

    void FixedUpdate()
    {
         rb.velocity = Vector2.left * velocity * Time.deltaTime;
        // Cambiar de color si está presionada
        spriteRenderer.color = isPressed ? Color.yellow : originalColor;
        //if (isSustainedNoteActive && Time.time - startTime >= sustainedDuration)
        //{
        //    isSustainedNoteActive = false;
        //    DeleteNote(); 
        //}
    }

    void Update()
    {
        if (isPressed)
        {
            sceneController.IncrementCombo();
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
       //isSustainedNoteActive = true;
    }

    public bool IsNearEnd()
    {
        // Verifica si la nota está cerca del final de su recorrido
        float noteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        float currentX = transform.position.x;
        float endX = transform.position.x + noteWidth;

        return Mathf.Abs(currentX - endX) <= endTolerance;
    }
    public void SetWidth(float width)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = width;
        transform.localScale = newScale;
    }
}

