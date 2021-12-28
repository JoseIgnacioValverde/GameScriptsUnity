using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullerTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeSlow, cooldown, duration;
    public bool timeSlowed;
    private float timeRemaining, cooldownRemaining;
    public AudioSource timeSlowAudio, timeNormalAudio;
    public Image timeStop, timeResume;
    void Start()
    {
        timeSlowed=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSlowed){
            ManageTimer();
        }
        ManageCooldown();
        
        
        if(Input.GetButtonDown("BulletTime")){
            Debug.Log("BulletTime");
            if(!timeSlowed){
                StartBulletTime();
                timeSlowed=true;
            }
            else{
                StopBulletTime();
                timeSlowed=false;
            }
        }
        if(timeRemaining<=0){
            StopBulletTime();
            timeSlowed=false;
        }
        if(cooldownRemaining<=0){
            timeResume.enabled=false;
            timeStop.enabled=true;
        }
        
    }
    void StartBulletTime(){
        if(cooldownRemaining<=0){
            Time.timeScale=timeSlow;
            Time.fixedDeltaTime = Time.timeScale*0.02f;
            cooldownRemaining=cooldown;
            timeRemaining=duration;
            timeStop.enabled=false;
            timeResume.enabled=true;
            timeSlowAudio.Play();
            timeNormalAudio.PlayDelayed(5f);
            
        }
            

    }
    void StopBulletTime(){
        
        Time.timeScale=1f;
        Time.fixedDeltaTime = Time.timeScale*0.02f;
        timeRemaining=0f;
        
    }
    void ManageTimer(){
        if(timeRemaining>0){
            timeRemaining-=Time.unscaledDeltaTime;
        }
        
    }
    void ManageCooldown(){
        if(cooldownRemaining>0){
            cooldownRemaining-=Time.unscaledDeltaTime;
        }
    
    }
}
