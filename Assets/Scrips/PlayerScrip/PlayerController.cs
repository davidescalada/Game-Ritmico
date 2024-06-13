using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float returnDelay;
    [SerializeField] float excellentRange = 0.35f;
    [SerializeField] float goodRange = 0.15f;
    [SerializeField] float perfectRange = 0.36f;

    public GameObject upperDetector;
    public GameObject centerDetector;
    public GameObject lowerDetector;

    private NoteDetector upperNoteDetector;
    private NoteDetector centerNoteDetector;
    private NoteDetector lowerNoteDetector;

    public static event Action<string> OnNoteCollided;

    void Start()
    {
        upperNoteDetector = upperDetector.GetComponent<NoteDetector>();
        centerNoteDetector = centerDetector.GetComponent<NoteDetector>();
        lowerNoteDetector = lowerDetector.GetComponent<NoteDetector>();
    }

    void Update()
    {
        CheckNoteKeys();
    }

    private void CheckNoteKeys()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ProcessNoteHit(upperNoteDetector);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ProcessNoteHit(lowerNoteDetector);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ProcessNoteHit(centerNoteDetector);
        }
    }

    private void ProcessNoteHit(NoteDetector noteDetector)
    {
        if (noteDetector.HasCollidingNotes())
        {
            Collider2D noteCollider = noteDetector.GetFirstCollidingNote();
            if (noteCollider != null)
            {
                float collisionTime = noteDetector.GetNoteCollisionTime(noteCollider);
                float hitTime = Mathf.Abs(Time.time - collisionTime); // Calcular hitTime desde la colisión
                Debug.Log("Hit Time: " + hitTime);

                if (hitTime <= goodRange)
                {
                    Debug.Log("BIEN");
                    OnNoteCollided?.Invoke("Bien");
                }
                else if (hitTime <= excellentRange)
                {
                    Debug.Log("EXCELENTE");
                    OnNoteCollided?.Invoke("¡Excelente!");
                }
                else if (hitTime >= perfectRange)
                {
                    Debug.Log("PERFECTO");
                    OnNoteCollided?.Invoke("Perfecto");
                }
                else
                {
                    Debug.Log("FALLO");
                    OnNoteCollided?.Invoke("Falló");
                }
                
                NotasController notasController = noteCollider.GetComponent<NotasController>();
                
                if (notasController != null)
                {
                    notasController.DeleteNote();
                }
            }
        }
    }
}

