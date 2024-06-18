using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorDeBotones : MonoBehaviour
{
    public GameObject objetoActual; // El objeto actual que será eliminado
    public GameObject nuevoObjeto;  // El nuevo objeto que será activado

    void Start()
    {
        // Asegurarse de que el botón tiene el evento asignado
        Button boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("El componente Button no está asignado en el objeto.");
        }
    }

    void OnButtonClick()
    {
        // Eliminar el objeto actual y su padre
        if (objetoActual != null)
        {
            Destroy(objetoActual.transform.parent.gameObject);
        }

        // Activar el nuevo objeto y su hijo
        if (nuevoObjeto != null)
        {
            nuevoObjeto.SetActive(true);
        }
    }
}

