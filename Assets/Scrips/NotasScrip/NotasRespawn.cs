using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasRespawn : MonoBehaviour
{
    public GameObject notePrefab; // Prefab de la nota
    public Vector2 upperPosition;
    public Vector2 lowerPosition ;
    public Vector2 centerPosition ;

    private Vector2[] spawnPositions;

    void Start()
    {
        // Inicializar las posiciones de respawn
        spawnPositions = new Vector2[] { upperPosition, lowerPosition, centerPosition };

        // Iniciar la corutina para el respawn de notas
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio entre 1 y 5 segundos
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);

            // Seleccionar una posición aleatoria
            Vector2 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

            // Instanciar la nota en la posición aleatoria
            Instantiate(notePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
