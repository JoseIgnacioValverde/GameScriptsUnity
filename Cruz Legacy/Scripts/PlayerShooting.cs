using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public float shootingForce=100;
    public float reloadTime;
    private float fireRate=0.75f;
    public Rigidbody arrow;
    public Transform puntaBallesta;
    public bool disparado=false;
    public AudioSource disparo;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Fire();
        }
        if(reloadTime>0){
            reloadTime-=Time.unscaledDeltaTime;
        }
         
    }
    private void Fire(){
        //disparado=true;
        
        if(reloadTime<=0f){
            disparo.Play();
            Rigidbody arrowInstance = Instantiate(arrow, puntaBallesta.position, puntaBallesta.rotation) as Rigidbody;
            arrowInstance.velocity = shootingForce*puntaBallesta.forward;
            reloadTime=1f/fireRate;
        }
       
        
    }
}
