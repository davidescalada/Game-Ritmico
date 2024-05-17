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
    [SerializeField] float returnDelay;
    private Vector2 targetPosition;
    private Rigidbody2D rb;
    public static event Action OnNoteCollided;
    private Coroutine moveCoroutine;
    private bool isCollidingWithNote = false;
    private Collider2D currentNoteCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = centerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        PosicionamientoInputs();
        CheckSpaceKey();
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
            isCollidingWithNote = true;     
            currentNoteCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Notas"))
        {
            isCollidingWithNote = false;
            currentNoteCollider = null;
        }
        
    }
    //private void PosicionamientoInputs()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        targetPosition = upperPosition;
    //    }
    //    else if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        targetPosition = lowerPosition;
    //    }
    //    else
    //    {
    //        targetPosition = centerPosition;
    //    }
    //}
    private void PosicionamientoInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveAndReturn(upperPosition));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveAndReturn(lowerPosition));
        }
    }

    private IEnumerator MoveAndReturn(Vector2 newPosition)
    {
        targetPosition = newPosition;
        yield return new WaitForSeconds(returnDelay);
        targetPosition = centerPosition;
    }

    private void CheckSpaceKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCollidingWithNote)
        {
             Debug.Log("Player colisiono con una nota");
             OnNoteCollided?.Invoke();

            if (currentNoteCollider != null)
            {
                NotasController notasController = currentNoteCollider.GetComponent<NotasController>();
                if (notasController != null)
                {
                    notasController.DeleteNote();
                }
            }
        }
    }
}
