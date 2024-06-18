using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusChat : MonoBehaviour
{
    public TMP_Text mensajeText; // Texto para mostrar el mensaje del virus
    public TMP_InputField inputField; // Campo de entrada para el usuario
    public Transform opcionesParent; // Padre donde se instanciarán las opciones de respuesta
    public GameObject opcionPrefab; // Prefab para las opciones
    public GameObject consolaSimulada;
    private int pasoActual = 0;
    private string mensajeAnterior;
    private List<string> opcionesAnteriores;

    // Lista de pasos con mensajes del virus y opciones de respuesta
    private List<ChatPaso> pasos = new List<ChatPaso>
    {
        new ChatPaso("Hola, te advierto que esto podría no ser un juego. ¿Aún así, deseas continuar?", new List<string> { "1. Sí", "2. No" }),
        new ChatPaso("¿Aceptas los terminos y condiciones?", new List<string> { "1. Continuar"}),
        new ChatPaso("Antes de continuar son necesarios algunos datos. ¿Deseas continuar?", new List<string> { "1. Sí", "2. No" })
    };

    void Start()
    {
        MostrarPasoActual();
        inputField.onSubmit.AddListener(ProcesarEntrada); // Listener para procesar la entrada del usuario
        inputField.Select();
        inputField.ActivateInputField();
    }

    void MostrarPasoActual()
    {
        if (pasoActual < pasos.Count)
        {
            mensajeText.text = pasos[pasoActual].mensaje;
            MostrarOpciones(pasos[pasoActual].opciones);
        }
        else
        {
            Debug.Log("Fin de la conversación.");
            // Aquí podrías habilitar el siguiente script, por ejemplo:
            //consolaSimulada.GetComponent<ConsolaSimulada>().enabled = true;
            consolaSimulada.gameObject.SetActive(true);
            gameObject.SetActive(false); // Desactivar el script actual
            mensajeText.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            opcionesParent.gameObject.SetActive(false);
        }
    }

    void MostrarOpciones(List<string> opciones)
    {
        // Limpiar opciones anteriores
        foreach (Transform child in opcionesParent)
        {
            Destroy(child.gameObject);
        }

        // Mostrar nuevas opciones (opcional, solo si quieres mostrar las opciones en pantalla)
        foreach (string opcion in opciones)
        {
            GameObject newOpcion = Instantiate(opcionPrefab, opcionesParent);
            TextMeshProUGUI opcionText = newOpcion.GetComponentInChildren<TextMeshProUGUI>();
            if (opcionText != null)
            {
                opcionText.text = opcion;
            }
            else
            {
                Debug.LogError("El prefab no tiene un componente TextMeshProUGUI.");
            }
        }
    }

    void ProcesarEntrada(string entrada)
    {
        Debug.Log("Entrada del usuario: " + entrada);
        // Procesar la entrada del usuario
        mensajeAnterior = mensajeText.text;
        opcionesAnteriores = new List<string>();
        foreach (Transform child in opcionesParent)
        {
            opcionesAnteriores.Add(child.GetComponentInChildren<TextMeshProUGUI>().text);
        }

        int opcionSeleccionada;
        if (int.TryParse(entrada, out opcionSeleccionada))
        {
            // Aquí puedes agregar la lógica para manejar cada opción
            switch (pasoActual)
            {
                case 0: // Paso 0: ¿Quieres continuar?
                    if (opcionSeleccionada == 1)
                    {
                        pasoActual++;
                        MostrarPasoActual();
                    }
                    else if (opcionSeleccionada == 2)
                    {
                        Debug.Log("Entro en el 2 del case 0");
                        mensajeText.text = "¡No puedes escapar ahora!";
                        MostrarOpciones(new List<string>());
                        StartCoroutine(RestaurarMensajeAnterior(2f));

                    }
                    else
                    {
                        mensajeText.text = "Ingrese una opción válida";
                        MostrarOpciones(pasos[pasoActual].opciones);
                        StartCoroutine(RestaurarMensajeAnterior(2f));
                    }
                    break;
                case 1:  
                    if (opcionSeleccionada == 1)
                    {
                        pasoActual++;
                        MostrarPasoActual();
                    }
                
                    else
                    {
                        Debug.Log("opcion no del case 1");
                        mensajeText.text = "Ingrese una opción válida";
                        MostrarOpciones(pasos[pasoActual].opciones);
                        StartCoroutine(RestaurarMensajeAnterior(2f));
                    }
                    break;
                case 2: 
                    if (opcionSeleccionada == 1)
                    {
                        pasoActual++;
                        MostrarPasoActual();
                    }
                    else if (opcionSeleccionada == 2)
                    {
                        mensajeText.text = "Acceso denegado: El sistema necesita la información.";
                        MostrarOpciones(new List<string>());
                        StartCoroutine(RestaurarMensajeAnterior(2f));
                    }
                    else
                    {
                        mensajeText.text = "Ingrese una opción válida";
                        MostrarOpciones(pasos[pasoActual].opciones);
                        StartCoroutine(RestaurarMensajeAnterior(2f));
                    }
                    break;
                default:
                    break;      
            }
        }
        else
        {
            Debug.Log("NNN");
        }

        inputField.text = ""; // Limpiar el campo de entrada
        inputField.Select();
        inputField.ActivateInputField();
    }

    IEnumerator RestaurarMensajeAnterior(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);

        // Restaurar el mensaje y las opciones anteriores
        mensajeText.text = mensajeAnterior;
        MostrarOpciones(opcionesAnteriores);
    }

    void OcultarOpciones()
    {
        foreach (Transform child in opcionesParent)
        {
            child.gameObject.SetActive(false);
        }
    }

}


[System.Serializable]
public class ChatPaso
{
    public string mensaje;
    public List<string> opciones;

    public ChatPaso(string mensaje, List<string> opciones)
    {
        this.mensaje = mensaje;
        this.opciones = opciones;
    }
}

