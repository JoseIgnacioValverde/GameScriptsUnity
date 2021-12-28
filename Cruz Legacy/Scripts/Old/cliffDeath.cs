using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliffDeath : MonoBehaviour
{
    public PlayerHealth playerHealth;
    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.tag=="Player"){
            playerHealth.hitpoints=0f;
        }
    }
    
}
