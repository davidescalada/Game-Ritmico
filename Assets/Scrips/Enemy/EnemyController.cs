using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void MoveLeft()
    {
        transform.Translate(Vector2.left * 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Gameplay3");
        }
    }
}
