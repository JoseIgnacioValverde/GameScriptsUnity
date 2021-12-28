using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Transform cabeza;
    public Transform torso;
    public Transform piernas;
    public bool golpeado;
    public float hitpoints = 1f;
    public float inmunityTime=0.5f;
    private float remainingInmunity;
    public LayerMask bullet;
    public EnemyMovement movement;
    public PlayerHealth playerHealth;
    private bool enemyDead=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        if(remainingInmunity>0){
            remainingInmunity-=Time.deltaTime;
        }
        else{
            golpeado=false;
            getHit();
            checkDeath();
        }
        
        
       
    }
    void getHit(){
        if(!golpeado){
        if(hitpoints>0){
            if(Physics.CheckSphere(cabeza.position, 0.58f, bullet)||Physics.CheckSphere(torso.position, 0.7f, bullet)||Physics.CheckSphere(piernas.position, 0.58f, bullet)){
                golpeado=true;
                hitpoints-=1f;
                remainingInmunity=inmunityTime;
            }
            
        }
    }
        
    }
    void checkDeath(){
        if(hitpoints<=0){
            movement.stopMoving();
            if(!enemyDead &&playerHealth.hitpoints<6f){
                playerHealth.gainHP(1.0f);
            }
            enemyDead=true;
            Destroy(gameObject,2.25f);
        }
    }
    public bool sendDeath(){
        return enemyDead;
    }
}