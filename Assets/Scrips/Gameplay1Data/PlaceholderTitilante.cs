using UnityEngine;
using System.Collections;
using TMPro;

public class PlaceholderTitilante : MonoBehaviour
{
    public TMP_InputField inputField; // Referencia al InputField que queremos controlar
    public TMP_Text placeholderText; // Referencia al placeholder del InputField
    public float titilacionSpeed = 0.5f; // Velocidad de titilación (segundos)

    private bool titilando = false;
    private Coroutine titilacionCoroutine;

    void Start()
    {
      
            // Asegurarse de que el placeholder tenga el color inicial correcto
            Color color = placeholderText.color;
            color.a = 1f; // Completamente visible
            placeholderText.color = color;

            // Iniciar la corutina para el efecto de titilación
            titilacionCoroutine = StartCoroutine(TitilarPlaceholder());

            // Suscribirse al evento OnSelect para detener la titilación cuando el campo esté enfocado
            inputField.onSelect.AddListener(DetenerTitilacion);
       

    }

    IEnumerator TitilarPlaceholder()
    {
        while (true)
        {
            // Alternar la opacidad entre visible e invisible
            float targetAlpha = titilando ? 1f : 0f;
            Color color = placeholderText.color;
            color.a = targetAlpha;
            placeholderText.color = color;

            // Cambiar el estado de titilación
            titilando = !titilando;

            // Esperar el tiempo especificado antes de alternar de nuevo
            yield return new WaitForSeconds(titilacionSpeed);
        }
    }

    void DetenerTitilacion(string text)
    {
        // Detener la corutina cuando el campo está enfocado
        if (titilacionCoroutine != null)
        {
            StopCoroutine(titilacionCoroutine);
        }

        // Asegurarse de que el placeholder sea completamente visible
        Color color = placeholderText.color;
        color.a = 1f;
        placeholderText.color = color;
    }

    void OnDestroy()
    {
        // Desuscribirse del evento cuando el objeto es destruido
        inputField.onSelect.RemoveListener(DetenerTitilacion);
    }
}



