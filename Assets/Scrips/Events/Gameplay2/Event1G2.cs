using UnityEngine;
using System.Collections;

public class Event1G2 : MonoBehaviour, IEvent
{
    public GameObject noiseEffect;
    private Animation[] noiseAnimations;

    void Start()
    {
        noiseAnimations = noiseEffect.GetComponentsInChildren<Animation>();
        noiseEffect.SetActive(false);
    }

    public void Execute()
    {
        StartCoroutine(ShowNoiseEffect());
    }

    private IEnumerator ShowNoiseEffect()
    {
        noiseEffect.SetActive(true); // Activar el efecto

        // Reproducir la animación en cada uno de los animadores
        foreach (Animation animation in noiseAnimations)
        {
            animation.Play();
        }

        yield return new WaitForSeconds(5f); // Esperar 5 segundos

        // Detener la animación y desactivar el efecto
        foreach (Animation animation in noiseAnimations)
        {
            animation.Stop();
        }
        noiseEffect.SetActive(false);
    }
}
