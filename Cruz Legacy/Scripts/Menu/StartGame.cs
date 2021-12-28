using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine("StartLevel1", other.gameObject);
        }
    }

    IEnumerator StartLevel1(GameObject player)
    {
        player.transform.position = transform.position;
        anim.SetBool("IsSceneChanging", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1"); //Pon el nombre de tu escena
    }
}
