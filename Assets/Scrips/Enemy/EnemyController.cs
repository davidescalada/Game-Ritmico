using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public float speed; // Velocidad del movimiento del enemigo
    public float moveDistance; // Distancia a mover cada vez que se llama a MoveLeft

    private Vector3 targetPosition;
    private bool isMoving = false;
    private SceneController sceneController;
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
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
        if (collision.CompareTag("Player")) 
        {
            speed = 0;
            Debug.Log("Choco con el player");
            if (sceneController != null)
            {
                sceneController.SetLlego(true);
            }
        }
    }
}
