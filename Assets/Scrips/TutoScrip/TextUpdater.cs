using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    // Lista de textos que queremos mostrar
    public string[] texts;
    private int currentIndex = 0;

    // Referencias a los componentes UI
    public TextMeshProUGUI textDisplay;
    public Button nextButton;
    public GameObject tutorialObject;
    public NotasRespawn notasRespawn;
    public AudioSource sfxAudioSource;

    void Start()
    {
        Time.timeScale = 0;
        if (notasRespawn != null)
        {
            notasRespawn.PauseMusic(); // Pausar la música
        }

        texts = new string[]
    {
            "Antes debo darte las siguientes instrucciones",
            "A la izquierda se encuentran los botones que activan las notas. ",
            "Se activan con las siguientes teclas. \nArriba: Boton arriba\nCentral: Space.\nAbajo: Flecha abajo.",
            "Recuerda presionarlo en el momento exacto, sino verás las consecuencias.", 
            "Por último, si logras alcanzarme, podras avanzar al siguiente reto."
    };
        // Asegúrate de que hay textos en la lista
        if (texts.Length == 0)
        {
            Debug.LogError("La lista de textos está vacía.");
            return;
        }

        // Mostrar el primer texto
        UpdateText();

        // Agregar el listener al botón
        nextButton.onClick.AddListener(NextText);
    }

    // Método para actualizar el texto en pantalla
    void UpdateText()
    {
        if (currentIndex < texts.Length)
        {
            textDisplay.text = texts[currentIndex];
        }
        else
        {
            // Si se supera el índice de la lista, ocultar el objeto del tutorial y reanudar la escena
            tutorialObject.SetActive(false);
            Time.timeScale = 1f;
            if (notasRespawn != null)
            {
                notasRespawn.ResumeMusic();
            }
        }
    }

    // Método para avanzar al siguiente texto
    void NextText()
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.Play();
        }
        currentIndex++;
        UpdateText();
    }
}

