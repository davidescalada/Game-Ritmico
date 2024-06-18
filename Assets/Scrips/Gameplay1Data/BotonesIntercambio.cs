using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class IntercambioBotones : MonoBehaviour
{
    public Button botonSiPrefab; // Prefab del bot�n si
    public Button botonNo; // Bot�n No original
    public string escenaDestino; // Nombre de la escena a la que se redirigir�
    private Button nuevoBotonNo;

    void Start()
    {
        // A�adir un listener al bot�n S� para cambiar de escena
        botonSiPrefab.onClick.AddListener(CambiarEscena);

        // A�adir un listener para el evento del puntero al bot�n No
        PointerHandler pointerHandler = botonNo.gameObject.AddComponent<PointerHandler>();
        pointerHandler.onPointerEnter += OnPointerEnterNo;
        pointerHandler.onPointerExit += OnPointerExitNo;
    }

    void OnPointerEnterNo()
    {
        // Crear un nuevo bot�n No en la posici�n del bot�n No original
        if (nuevoBotonNo == null)
        {
            nuevoBotonNo = Instantiate(botonSiPrefab, botonNo.transform.position, botonNo.transform.rotation, botonNo.transform.parent);

            // A�adir un listener para los eventos del puntero al nuevo bot�n No
            PointerHandler newPointerHandler = nuevoBotonNo.gameObject.AddComponent<PointerHandler>();
            newPointerHandler.onPointerExit += OnPointerExitNewNo;
        }

        // Ocultar el bot�n No original
        botonNo.gameObject.SetActive(false);
    }

    void OnPointerExitNo()
    {
        // Si el nuevo bot�n No no existe, volver a mostrar el bot�n No original
        if (nuevoBotonNo == null)
        {
            botonNo.gameObject.SetActive(true);
        }
    }

    void OnPointerExitNewNo()
    {
        // Destruir el nuevo bot�n No y volver a mostrar el bot�n No original
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

