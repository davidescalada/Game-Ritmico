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
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
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
}
