using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsolaSimulada : MonoBehaviour
{
    public TMP_Text instruccionesText;
    public GameObject inputFieldPrefab; // Prefab del InputField
    public Transform inputFieldParent;  // El padre donde se instanciarán los nuevos InputFields
    public TMP_Text resultadoText;
    public List<Vector2> posicionesInputFields; // Lista de posiciones para los InputFields
    public GameObject objetoAActivar;
    public AudioSource audioSource; // AudioSource para los efectos de sonido
    public AudioClip sonidoActivacion; // Efecto de sonido para la activación

    private enum Paso
    {
        Bienvenida,
        Nombre,
        Telefono,
        Correo,
        Completo
    }

    private Paso pasoActual = Paso.Bienvenida;
    private string nombre = "";
    private string telefono = "";
    private string correo = "";
    private int indicePosicionActual = 0; // Índice de la posición actual en la lista

    void Start()
    {
        MostrarPasoActual();
    }

    void MostrarPasoActual()
    {
        switch (pasoActual)
        {
            case Paso.Bienvenida:
                instruccionesText.text = "Si quieres ingresar, llena los datos.";
                SiguientePaso();
                break;
            case Paso.Nombre:
                instruccionesText.text = "Nombre:";
                InstanciarInputField();
                break;
            case Paso.Telefono:
                instruccionesText.text = $"Nombre: {nombre}\n\nTelefono:";
                InstanciarInputField();
                break;
            case Paso.Correo:
                instruccionesText.text = $"Nombre: {nombre}\n\nTelefono: {telefono}\n\nCorreo:";
                InstanciarInputField();
                break;
            case Paso.Completo:
                StartCoroutine(MostrarMensajeFinal());
                break;
            default:
                break;
        }
    }

    public void SiguientePaso()
    {
        switch (pasoActual)
        {
            case Paso.Bienvenida:
                pasoActual = Paso.Nombre;
                break;
            case Paso.Nombre:
                nombre = GetInputFieldValue();
                pasoActual = Paso.Telefono;
                break;
            case Paso.Telefono:
                telefono = GetInputFieldValue();
                pasoActual = Paso.Correo;
                break;
            case Paso.Correo:
                correo = GetInputFieldValue();
                instruccionesText.text = $"Nombre: {nombre}\n\nTelefono: {telefono}\n\nCorreo: {correo}";
                pasoActual = Paso.Completo;
                // Eliminar el último InputField después de ingresar el correo
                DestruirUltimoInputField();
                break;
            default:
                break;
        }

        // Solo mostrar el siguiente paso si no estamos en el paso "Completo"
        if (pasoActual != Paso.Completo)
        {
            MostrarPasoActual();
        }
        else
        {
            StartCoroutine(MostrarMensajeFinal());
        }
    }

    void InstanciarInputField()
    {
        if (inputFieldPrefab != null && inputFieldParent != null)
        {
            // Destruir los campos de entrada anteriores para evitar acumulación
            foreach (Transform child in inputFieldParent)
            {
                Destroy(child.gameObject);
            }

            GameObject newInputFieldObject = Instantiate(inputFieldPrefab, inputFieldParent);
            TMP_InputField newInputField = newInputFieldObject.GetComponent<TMP_InputField>();
            if (newInputField != null)
            {
                // Posicionar el InputField según la lista de posiciones
                RectTransform inputFieldRect = newInputField.GetComponent<RectTransform>();
                inputFieldRect.anchoredPosition = posicionesInputFields[indicePosicionActual];
                indicePosicionActual++; // Avanzar al siguiente índice de posición

                newInputField.onSubmit.AddListener(delegate { SiguientePaso(); });
                newInputField.Select();
                newInputField.ActivateInputField();
            }
            else
            {
                Debug.LogError("El prefab de InputField no tiene un componente InputField.");
            }
        }
        else
        {
            Debug.LogError("inputFieldPrefab o inputFieldParent no está asignado en el inspector.");
        }
    }

    string GetInputFieldValue()
    {
        TMP_InputField[] inputFields = inputFieldParent.GetComponentsInChildren<TMP_InputField>();
        if (inputFields.Length > 0)
        {
            return inputFields[inputFields.Length - 1].text;
        }
        return "";
    }

    void DestruirUltimoInputField()
    {
        TMP_InputField[] inputFields = inputFieldParent.GetComponentsInChildren<TMP_InputField>();
        if (inputFields.Length > 0)
        {
            Destroy(inputFields[inputFields.Length - 1].gameObject);
        }
    }

    IEnumerator MostrarMensajeFinal()
    {
        string mensajeFinal = "Gracias por tus datos, la verdad es que no fue tan difícil engañarte, para la próxima deberías tener más cuidado. No intentes ir tras mi, no tendrás oportunidad de alcanzarme.";
        string puntosSuspensivos = "... @#*!:°#&((!((#$#$!°°$#$%#/)$&#°°**¨[_* .....";

        resultadoText.text = "";

        // Mostrar los puntos suspensivos y signos raros
        foreach (char c in puntosSuspensivos)
        {
            resultadoText.text += c;
            yield return new WaitForSeconds(0.2f); // Esperar un tiempo entre cada carácter
        }

        yield return new WaitForSeconds(5f); // Esperar 5 segundos

        // Limpiar el texto antes de mostrar el mensaje final
        resultadoText.text = "";

        // Mostrar el mensaje final carácter por carácter
        foreach (char c in mensajeFinal)
        {
            resultadoText.text += c;
            yield return new WaitForSeconds(0.1f); // Esperar un tiempo entre cada carácter
        }

        // Activar el objeto y su hijo al finalizar el mensaje
        yield return new WaitForSeconds(2f);
        if (objetoAActivar != null & sonidoActivacion != null)
        {
            audioSource.PlayOneShot(sonidoActivacion);
           
        }
        objetoAActivar.SetActive(true);
    }
}




