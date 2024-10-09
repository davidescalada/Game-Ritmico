using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TMP_Text textCombo;
    public TMP_Text feedbackText;
    public int contadorCombo = 0;
    public EnemyController enemyController;
    private NotasRespawn notasRespawn;
    private bool llego = false;
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        notasRespawn = FindObjectOfType<NotasRespawn>();

        // Suscribirse al evento OnNotesFinished
        if (notasRespawn != null)
        {
            notasRespawn.OnNotesFinished += HandleNotesFinished;
        }
    }

    void Update()
    {
        ReloadScene();
    }

    public void ReloadScene()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene("GameMain");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnEnable()
    {
        PlayerController.OnNoteCollided += HandleNoteCollision;
    }

    private void OnDisable()
    {
        PlayerController.OnNoteCollided -= HandleNoteCollision;
    }

    private void HandleNoteCollision(string result)
    {
        ShowFeedback(result);

        if (result == "¡Excelente!" || result == "Bien" || result == "Por poco")
        {
            IncrementCombo();
            enemyController.MoveLeft();
        }
        else if (result == "Falló")
        {
            ResetCombo();
        }
    }

    public void IncrementCombo()
    {
        contadorCombo++;
        UpdatedComboText();
    }

    public void ResetCombo()
    {
        contadorCombo = 0;
        UpdatedComboText();
    }

    private void UpdatedComboText()
    {
        textCombo.text = "Combo: " + contadorCombo.ToString();
    }

    private void ShowFeedback(string result)
    {
        feedbackText.text = result;
        StartCoroutine(ClearFeedback());
    }

    private IEnumerator ClearFeedback()
    {
        yield return new WaitForSeconds(0.5f);
        feedbackText.text = "";
    }

    private void HandleNotesFinished()
    {
        // Aquí manejamos el final de la emisión de notas
        Debug.Log("La emisión de notas ha terminado.");
        Invoke("CheckLlego", 3.0f);
    }

    public void SetLlego(bool value)
    {
        llego = value;
    }

    private void CheckLlego()
    {
        if (llego)
        {
            Debug.Log("Cambiando a escena 3");
            SceneManager.LoadScene("Gameplay3");
        }
        else
        {
            SceneManager.LoadScene("GameMain");
        }
    }
}
