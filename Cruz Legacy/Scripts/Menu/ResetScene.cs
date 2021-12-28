using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Start(){
        Cursor.lockState=CursorLockMode.None;
    }
    public void quitGame(){
        Application.Quit();
    }
}
    
