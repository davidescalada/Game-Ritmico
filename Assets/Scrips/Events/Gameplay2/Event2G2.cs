using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2G2 : MonoBehaviour, IEvent
{
    public float vibrationDuration = 5f;
    public float vibrationIntensity = 0.1f;

    public void Execute()
    {
        StartCoroutine(VibrateScreen());
    }

    private IEnumerator VibrateScreen()
    {
        Vector3 originalPosition = Camera.main.transform.position;

        for (float t = 0; t < vibrationDuration; t += Time.deltaTime)
        {
            float x = Random.Range(-vibrationIntensity, vibrationIntensity);
            float y = Random.Range(-vibrationIntensity, vibrationIntensity);

            Camera.main.transform.position = originalPosition + new Vector3(x, y, 0);
            yield return null;
        }

        Camera.main.transform.position = originalPosition;
    }
}