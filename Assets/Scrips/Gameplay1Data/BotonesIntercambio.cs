using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class IntercambioBotones : MonoBehaviour
{
    public Button botonSiPrefab; // Prefab del botón si
    public Button botonNo; // Botón No original
    public string escenaDestino; // Nombre de la escena a la que se redirigirá
    private Button nuevoBotonNo;

    void Start()
    {
        // Añadir un listener al botón Sí para cambiar de escena
        botonSiPrefab.onClick.AddListener(CambiarEscena);

        // Añadir un listener para el evento del puntero al botón No
        PointerHandler pointerHandler = botonNo.gameObject.AddComponent<PointerHandler>();
        pointerHandler.onPointerEnter += OnPointerEnterNo;
        pointerHandler.onPointerExit += OnPointerExitNo;
    }

    void OnPointerEnterNo()
    {
        // Crear un nuevo botón No en la posición del botón No original
        if (nuevoBotonNo == null)
        {
            nuevoBotonNo = Instantiate(botonSiPrefab, botonNo.transform.position, botonNo.transform.rotation, botonNo.transform.parent);

            // Añadir un listener para los eventos del puntero al nuevo botón No
            PointerHandler newPointerHandler = nuevoBotonNo.gameObject.AddComponent<PointerHandler>();
            newPointerHandler.onPointerExit += OnPointerExitNewNo;
        }

        // Ocultar el botón No original
        botonNo.gameObject.SetActive(false);
    }

    void OnPointerExitNo()
    {
        // Si el nuevo botón No no existe, volver a mostrar el botón No original
        if (nuevoBotonNo == null)
        {
            botonNo.gameObject.SetActive(true);
        }
    }

    void OnPointerExitNewNo()
    {
        // Destruir el nuevo botón No y volver a mostrar el botón No original
        if (nuevoBotonNo != null)
        {
            Destroy(nuevoBotonNo.gameObject);
            nuevoBotonNo = null;
        }
        botonNo.gameObject.SetActive(true);
    }

    void CambiarEscena()
    {
        // Redirigir a la escena especificada
        SceneManager.LoadScene("GameMain");
    }
}

