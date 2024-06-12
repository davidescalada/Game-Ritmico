using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetector : MonoBehaviour
{
    public string position; // "Upper", "Center", or "Lower"
    private List<Collider2D> collidingNotes = new List<Collider2D>();
    private Dictionary<Collider2D, float> noteCollisionTimes = new Dictionary<Collider2D, float>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Notas"))
        {
            collidingNotes.Add(collision);
            noteCollisionTimes[collision] = Time.time; // Registrar el tiempo de colisión
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Notas"))
        {
            collidingNotes.Remove(collision);
            noteCollisionTimes.Remove(collision); // Eliminar el tiempo de colisión
        }
    }

    public bool HasCollidingNotes()
    {
        return collidingNotes.Count > 0;
    }

    public Collider2D GetFirstCollidingNote()
    {
        foreach (var note in collidingNotes)
        {
            return note;
        }
        return null;
    }
    public float GetNoteCollisionTime(Collider2D noteCollider)
    {
        if (noteCollisionTimes.ContainsKey(noteCollider))
        {
            return noteCollisionTimes[noteCollider];
        }
        return 0f;
    }
}
