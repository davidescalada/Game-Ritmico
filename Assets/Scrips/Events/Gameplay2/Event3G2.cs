using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Event3G2 : MonoBehaviour, IEvent
{
    public GameObject imagePrefab; // Prefab con un componente Image
    public List<Sprite> images; // Lista de sprites que se mostrar�n
    [SerializeField] float effectDuration; // Duraci�n del efecto total
    [SerializeField] float imageDuration; // Duraci�n que cada imagen estar� visible
    [SerializeField] float cooldown; // Tiempo entre cada imagen

    private Canvas canvas;

    private void Start()
    {
        // Buscar o crear un Canvas en la escena si no existe
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("Canvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }
    }

    public void Execute()
    {
        StartCoroutine(ShowRandomImages());
    }

    private IEnumerator ShowRandomImages()
    {
        float elapsedTime = 0f;

        while (elapsedTime < effectDuration)
        {
            ShowRandomImage();
            yield return new WaitForSeconds(imageDuration + cooldown);
            elapsedTime += imageDuration + cooldown;
        }
    }

    private void ShowRandomImage()
    {
        if (images.Count == 0 || imagePrefab == null)
        {
            Debug.LogWarning("No hay im�genes o prefab asignado.");
            return;
        }

        // Crear la imagen en una posici�n aleatoria
        GameObject newImageObject = Instantiate(imagePrefab, canvas.transform);
        RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();

        // Configurar tama�o y posici�n aleatoria dentro de la pantalla
        rectTransform.anchoredPosition = new Vector2(
            Random.Range(-Screen.width / 2f, Screen.width / 2f),
            Random.Range(-Screen.height / 2f, Screen.height / 2f)
        );

        // Asignar un sprite aleatorio
        Image imageComponent = newImageObject.GetComponent<Image>();
        imageComponent.sprite = images[Random.Range(0, images.Count)];

        // Destruir la imagen despu�s de un tiempo
        Destroy(newImageObject, imageDuration);
    }
}



