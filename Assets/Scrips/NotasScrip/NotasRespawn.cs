using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class NotasRespawn : MonoBehaviour
{
    public GameObject notePrefab; // Prefab de la nota
    public Vector2 upperPosition;
    public Vector2 lowerPosition;
    public Vector2 centerPosition;
    public AudioSource audioSource; // El AudioSource que reproduce la canción

    public float spawnInterval; // Intervalo entre spawns
    public float delayBeforeMusic; // Retraso antes de comenzar la música
    public float[] targetFrequencies; // Frecuencias objetivo para las notas
    public float frequencyTolerance; // Tolerancia en Hz para la detección de frecuencias
    public float minNoteInterval; // Intervalo mínimo entre la generación de notas para cada frecuencia objetivo
    public float intervalDecreaseAmount = 0.01f; // Cantidad por la que disminuir el intervalo cada 10 segundos


    private Dictionary<float, float> lastNoteTimes = new Dictionary<float, float>(); // Registro de los últimos tiempos de generación de notas para cada frecuencia objetivo
    private float[] spectrum = new float[1024];
    private int beatIndex = 0;

    void Start()
    {
        // Inicializar los últimos tiempos de generación de notas para cada frecuencia objetivo
        foreach (float frequency in targetFrequencies)
        {
            lastNoteTimes[frequency] = -minNoteInterval; // Inicializar con un valor que garantice que la primera nota se generará inmediatamente
        }

        // Iniciar la corutina para el respawn de notas
        StartCoroutine(SpawnNotes());

        // Iniciar la música después de un retraso
        StartCoroutine(StartMusicAfterDelay());

        // Iniciar la corutina para disminuir gradualmente el valor de minNoteInterval después de 50 segundos
        StartCoroutine(DecreaseMinNoteIntervalGradually(30.0f, 50.0f, intervalDecreaseAmount));
    }

    void Update()
    {
        GetSpectrumAudioSource();
        Debug.Log("minNoteInterval: " + minNoteInterval);
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // Comparar las frecuencias detectadas con las frecuencias objetivo
        for (int i = 0; i < targetFrequencies.Length; i++)
        {
            float frequency = targetFrequencies[i];
            if (IsFrequencyDetected(frequency) && Time.time - lastNoteTimes[frequency] > minNoteInterval)
            {
                Vector2 spawnPosition = GetSpawnPosition(i);
                Instantiate(notePrefab, spawnPosition, Quaternion.identity);
                lastNoteTimes[frequency] = Time.time; // Actualizar el último tiempo de generación de nota para esta frecuencia
            }
        }
    }

    bool IsFrequencyDetected(float frequency)
    {
        float frequencyResolution = AudioSettings.outputSampleRate / 2 / spectrum.Length;
        int targetIndex = Mathf.RoundToInt(frequency / frequencyResolution);
        int minIndex = Mathf.RoundToInt((frequency - frequencyTolerance) / frequencyResolution);
        int maxIndex = Mathf.RoundToInt((frequency + frequencyTolerance) / frequencyResolution);

        // Verificar si la frecuencia objetivo está dentro del rango del espectro
        minIndex = Mathf.Clamp(minIndex, 0, spectrum.Length - 1);
        maxIndex = Mathf.Clamp(maxIndex, 0, spectrum.Length - 1);

        // Determinar si alguna frecuencia en el rango tiene amplitud significativa
        float threshold = 0.1f;
        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (spectrum[i] > threshold)
            {
                return true;
            }
        }
        return false;
    }

    Vector2 GetSpawnPosition(int frequencyIndex)
    {
        // Devolver la posición de spawn correspondiente a la frecuencia detectada
        switch (frequencyIndex)
        {
            case 0:
                return upperPosition;
            case 1:
                return lowerPosition;
            case 2:
                return centerPosition;
            default:
                return Vector2.zero;
        }
    }

    IEnumerator SpawnNotes()
    {
        while (true)
        {
            // Actualizar el índice de beat si es necesario
            if (Time.time >= beatIndex * spawnInterval)
            {
                beatIndex++;
            }
            yield return null; // Esperar hasta el siguiente frame
        }
    }

    IEnumerator StartMusicAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMusic);
        audioSource.Play();
    }

    IEnumerator DecreaseMinNoteIntervalGradually(float initialDelay, float duration, float decreaseAmount)
    {
        yield return new WaitForSeconds(initialDelay);

        float elapsedTime = 0f;
        float intervalDecreaseRate = decreaseAmount / duration;

        while (elapsedTime < duration)
        {
            minNoteInterval -= intervalDecreaseRate;
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f); // Esperar un segundo entre cada decremento
        }
    }
}














