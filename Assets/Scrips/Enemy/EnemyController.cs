using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f; // Velocidad del movimiento del enemigo
    public float moveDistance = 10.0f; // Distancia a mover cada vez que se llama a MoveLeft

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Inicialmente, la posición objetivo es la misma que la posición actual
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void MoveLeft()
    {
        // Establecer la nueva posición objetivo hacia la izquierda
        targetPosition = transform.position + Vector3.left * moveDistance;
        isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        // Movimiento suave hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Detener el movimiento si se alcanza la posición objetivo
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Gameplay3");
        }
    }
}
