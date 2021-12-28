using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement movement;
    public float hitpoints = 6f;
    public float inmunityTime=0.5f;
    private float remainingInmunity;
    public LayerMask voidMask,bullet;
    public Image[] healthImages;
    public CharacterController controller;
    public Transform cabeza;
    public Transform torso;
    public Transform piernas;
    public bool golpeado, playerDied;
    public AudioSource playerHit;
    public AudioSource playerDead;

    // Start is called before the first frame update
    void Start()
    {
        playerDied=false;
    }

    // Update is called once per frame
    void fixedUpdate(){
        
    }
    void Update()
    {
        if(remainingInmunity>0){
            remainingInmunity-=Time.deltaTime;
        }
        else{
            golpeado=false;
            getHit();
        }
        
        checkDeath();
       manageHealthImage();
       
    }
    void manageHealthImage(){
        switch(hitpoints){
           case 6f:
           healthImages[0].enabled=true;
           healthImages[1].enabled=false;
           healthImages[2].enabled=false;
           healthImages[3].enabled=false;
           healthImages[4].enabled=false;
           healthImages[5].enabled=false;
           healthImages[6].enabled=false;
           break;
           case 5f:
            healthImages[0].enabled=false;
           healthImages[1].enabled=true;
           healthImages[2].enabled=false;
           healthImages[3].enabled=false;
           healthImages[4].enabled=false;
           healthImages[5].enabled=false;
           healthImages[6].enabled=false;
           break;
           case 4f:
           healthImages[0].enabled=false;
           healthImages[1].enabled=false;
           healthImages[2].enabled=true;
           healthImages[3].enabled=false;
           healthImages[4].enabled=false;
           healthImages[5].enabled=false;
           healthImages[6].enabled=false;
           break;
           case 3f:
           healthImages[0].enabled=false;
           healthImages[1].enabled=false;
           healthImages[2].enabled=false;
           healthImages[3].enabled=true;
           healthImages[4].enabled=false;
           healthImages[5].enabled=false;
           healthImages[6].enabled=false;
           break;
           case 2f:
           healthImages[0].enabled=false;
           healthImages[1].enabled=false;
           healthImages[2].enabled=false;
           healthImages[3].enabled=false;
           healthImages[4].enabled=true;
           healthImages[5].enabled=false;
           healthImages[6].enabled=false;
           break;
           case 1f:
           healthImages[0].enabled=false;
           healthImages[1].enabled=false;
           healthImages[2].enabled=false;
           healthImages[3].enabled=false;
           healthImages[4].enabled=false;
           healthImages[5].enabled=true;
           healthImages[6].enabled=false;
           break;
           case 0f:
           healthImages[0].enabled=false;
           healthImages[1].enabled=false;
           healthImages[2].enabled=false;
           healthImages[3].enabled=false;
           healthImages[4].enabled=false;
           healthImages[5].enabled=false;
           healthImages[6].enabled=true;
           break;

       } 
    }
    void getHit(){
        if(!golpeado){
        if(hitpoints>0){
            if(Physics.CheckSphere(cabeza.position, 0.58f, bullet)||Physics.CheckSphere(torso.position, 0.58f, bullet)||Physics.CheckSphere(piernas.position, 0.58f, bullet)){
                if(hitpoints>1){
                     playerHit.Play();
                }
               
                golpeado=true;
                hitpoints-=1f;
                remainingInmunity=inmunityTime;
                

            }
            
        }
    }
        
    }
    void checkDeath(){
        if(Physics.CheckSphere(movement.tomaDeTierra.position, movement.distanciaDelSuelo, voidMask)){
            hitpoints=0f;
        }
        if(hitpoints<=0f){
            playerDead.Play();
            playerDied=true;
        }
    }
    public void gainHP(float health){
        hitpoints=hitpoints+health;
    }

}
