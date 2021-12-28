using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Animator anim;

    void Start(){
        Cursor.lockState=CursorLockMode.None;
    }

    public void StartGame()
    {
        StartCoroutine("ChangingScene");
    }

    public void ExitGame()
    {
        StartCoroutine("QuitGame");
    }

    IEnumerator ChangingScene()
    {
        anim.SetBool("IsSceneChanging", true); //Pon el nombre de  tu variable
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Escenario"); //Pon el nombre de tu escena
    }

    IEnumerator QuitGame()
    {
        anim.SetBool("IsSceneChanging", true); //Pon el nombre de  tu variable
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
