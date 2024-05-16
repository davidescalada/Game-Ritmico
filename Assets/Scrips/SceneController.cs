using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
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
}
