using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneController : MonoBehaviour
{
    
    public TMP_Text textCombo;
    public int contadorCombo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        PlayerController.OnNoteCollided += IncrementCombo;
    }

    private void OnDisable()
    {
        PlayerController.OnNoteCollided -= IncrementCombo;
    }
    private void IncrementCombo()
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
}
