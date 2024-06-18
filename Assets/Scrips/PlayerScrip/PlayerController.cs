using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float returnDelay;
    [SerializeField] float goodRange;
    [SerializeField] float perfectRange;
    [SerializeField] float almostRange;

    public GameObject sustainedNoteDetectorObject;
    public GameObject upperDetector;
    public GameObject lowerDetector;

    private NoteDetector upperNoteDetector;
    private NoteDetector lowerNoteDetector;

    public static event Action<string> OnNoteCollided;

    private SustainedNoteDetector sustainedNoteDetector;
    private SustainedNoteController activeSustainedNote;

    void Start()
    {
        upperNoteDetector = upperDetector.GetComponent<NoteDetector>();
        lowerNoteDetector = lowerDetector.GetComponent<NoteDetector>();
        sustainedNoteDetector = sustainedNoteDetectorObject.GetComponent<SustainedNoteDetector>();
    }

    void Update()
    {
        CheckNoteKeys();
        CheckSustainedNoteEnd();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSustainedNote();
        }
    }

    private void StartSustainedNote()
    {
        if (activeSustainedNote == null && sustainedNoteDetector.HasCollidingNotes())
        {
            Collider2D noteCollider = sustainedNoteDetector.GetFirstCollidingNote();
            if (noteCollider != null)
            {
                SustainedNoteController sustainedNote = noteCollider.GetComponent<SustainedNoteController>();
                if (sustainedNote != null)
                {
                    activeSustainedNote = sustainedNote;
                    activeSustainedNote.SetPressed(true);
                    activeSustainedNote.StartSustainedNote();
                }
            }
        }
    }

    private void CheckSustainedNoteEnd()
    {
        if (activeSustainedNote != null)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                activeSustainedNote.SetPressed(false);
                if (activeSustainedNote.IsNearEnd())
                {
                    Debug.Log("EXCELENTE");
                    OnNoteCollided?.Invoke("¡Excelente!");
                }
                else
                {
                    Debug.Log("FALLO");
                    OnNoteCollided?.Invoke("Falló");
                }
                activeSustainedNote.DeleteNote();
                activeSustainedNote = null;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                OnNoteCollided?.Invoke("¡Excelente!");
            }
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
                float hitTime = Mathf.Abs(Time.time - collisionTime);

                Debug.Log($"Checking hit time: {hitTime}");
                Debug.Log($"goodRange: {goodRange}, perfectRange: {perfectRange}, almostRange: {almostRange}");

                if (hitTime <= goodRange)
                {
                    Debug.Log("BIEN");
                    OnNoteCollided?.Invoke("Bien");
                }
                else if (hitTime <= perfectRange)
                {
                    Debug.Log("EXCELENTE");
                    OnNoteCollided?.Invoke("¡Excelente!");
                }
                else if (hitTime <= almostRange)
                {
                    Debug.Log("Por poco");
                    OnNoteCollided?.Invoke("Por poco");
                }

                NotasController notasController = noteCollider.GetComponent<NotasController>();

                if (notasController != null)
                {
                    notasController.DeleteNote();
                }
            }
            else
            {
                Debug.Log("FALLO - No hay nota en la colisión");
                OnNoteCollided?.Invoke("Falló");
            }
        }
        else
        {
            Debug.Log("FALLO - No hay nota en la colisión");
            OnNoteCollided?.Invoke("Falló");
        }
    }
}


