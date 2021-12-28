using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CustomGameManager : MonoBehaviour
{
    public GameObject rejilla1, rejilla2, rejilla3, rejilla4, rejilla5, rejilla6, rejilla7, rejilla8;
    public int killCount;
    public PlayerHealth playerHealth;
    public Canvas HUD, Menu, HookUnlock, BulletTimeUnlocked;
    public GameObject fork, magicsphere;
    public Transform forkpos, magicspherepos, endplace;
    public PlayerLook playerLook;
    public PlayerShooting shooting;
    private Scene scene;
    public hook playerHook;
    public BullerTime playertime;
    private bool collectedFork, collectedSphere;
    public LayerMask playerMask;
    public TextMeshProUGUI text;
    public Image timeStop, timeResume;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Cursor.lockState=CursorLockMode.Locked;
        HUD.enabled=true;
        Menu.enabled=false;
        HookUnlock.enabled=false;
        BulletTimeUnlocked.enabled=false;
        collectedFork=false;
        collectedSphere=false;
        playerHook.enabled=false;
        playertime.enabled=false;
        timeStop.enabled=false;
        timeResume.enabled=false;
        killCount=0;
        Time.timeScale=1f;
        text.text = "Explore the dungeon";
    }

    // Update is called once per frame
    void Update()
    {
        checkCollectedItem();
        checkGameState();
        checkPlayerDead();
    }
    void checkGameState(){
        switch(killCount){
            case(6):
            Destroy(rejilla1);
            text.text = "Get deeper into the dungeon";
            break;
            case(13):
            Destroy(rejilla2);
            text.text = "Stop the time ritual";
            break;
            case(27):
            Destroy(rejilla3);
            Destroy(rejilla4);
            Destroy(rejilla5);
            text.text = "Kill all the enemies";
            break;
            case(54):
            Destroy(rejilla6);
            Destroy(rejilla7);
            Destroy(rejilla8);
            text.text = "Go to the next floor";
            break;
        }
    }
    public void checkPlayerDead(){
        if(playerHealth.playerDied){
            PauseTime();
            HUD.enabled=false;
            Menu.enabled=true;
            Cursor.lockState=CursorLockMode.None;
            playerLook.enabled=false;
            shooting.enabled=false;

        }
    }
    void PauseTime(){
        Time.timeScale=0f;
        playerLook.enabled=false;
        shooting.enabled=false;
        
    }
    public void ResumeTime(){
        HookUnlock.enabled=false;
        BulletTimeUnlocked.enabled=false;
        playerLook.enabled=true;
        shooting.enabled=true;
        Cursor.lockState=CursorLockMode.Locked;
        Time.timeScale=1f;
    }
    public void resetLevel(){
        SceneManager.LoadScene(scene.name);
    }
    public void quitGame(){
        Application.Quit();
    }
    void checkCollectedItem(){
        if(!collectedFork){
            if(Physics.CheckSphere(forkpos.position,5f,playerMask)){
            collectedFork=true;
            Destroy(fork);
            PauseTime();
            playerHook.enabled=true;
            HookUnlock.enabled=true;
            Menu.enabled=false;
            Cursor.lockState=CursorLockMode.None;
            
            }
        }
         if(!collectedSphere){
            if(Physics.CheckSphere(magicspherepos.position,5f,playerMask)){
            collectedSphere=true;
            Destroy(magicsphere);
            PauseTime();
            playertime.enabled=true;
            BulletTimeUnlocked.enabled=true;
            timeStop.enabled=true;
            timeResume.enabled=true;
            Menu.enabled=false;
            Cursor.lockState=CursorLockMode.None;
            
            }
        }
        if(Physics.CheckSphere(endplace.position,5f,playerMask)){
            SceneManager.LoadScene("Final Scene");
        }

        
    }

}

