using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovRB : MonoBehaviour
{
    public Rigidbody rb;
    //movimiento
    public float speed = 4500f;
    public float maxSpeed = 20f;
    public bool tocaSuelo;
    public LayerMask groundMask;
    
    //control de tamaños para el agachado
    private Vector3 playerScale;
    private Vector3 crouchingScale = new Vector3(1, 0.5f, 1);
    //deslizamiento
    private float slideForce = 500f;
    private Vector3 vectorNormal = Vector3.up;

    //salto
    private bool listoParaSaltar;
    private float enfriamientoSalto=0f;//bajar a cero si al final pongo bunnyhop
    public float jumpForce =500f;

    //inputs
    private float x, z;
    public bool agachado, saltando, sprintando;
    
    //precision
    private float countermovement = 0.175f;
    private float threshhold= 0.01f;
    public float maxSlopeAngle=30f;

    //extras. los he tenido que ir añadiendo para chapuzas
    private bool cancellSuelo;
    
    void Awake()
    {
      
    }
    void Start(){
        agachado = false;
       listoParaSaltar=false;
       saltando=false;
       tocaSuelo=true; 
       sprintando = false;
       playerScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
       Inputs();
    }
    void FixedUpdate(){
        Movimiento();
    }
    void Movimiento(){

        rb.AddForce(Vector3.down *Time.deltaTime*10);//gravedad extra

        //Vector2 mag =VeloctorRelativoMira();

        rb.AddForce(transform.right * x * speed * Time.deltaTime);
        rb.AddForce(transform.forward * z * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump")&& listoParaSaltar){
            saltar();
        }

    }
    void Inputs(){

        x=Input.GetAxis("Horizontal");
        z=Input.GetAxis("Vertical");
        saltando = Input.GetButtonDown("Jump");

        if(Input.GetButtonDown("Clutch")){
            if(!agachado){
                agacharse();
                agachado=true;
            }
            else{
                levantarse();
                agachado=false;
            }
        }
       
    }
    void agacharse(){


        transform.localScale= crouchingScale;
        

    }
    void levantarse(){

       transform.localScale= playerScale;

    }
    void saltar(){
        if(listoParaSaltar){
            listoParaSaltar=false;

            //fuerzas de salto
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(vectorNormal * jumpForce * 0.5f);

            Vector3 vel =rb.velocity;
            if(rb.velocity.y <0.5f){//arreglo para cuando se intenta saltar en el aire
                rb.velocity= new Vector3(vel.x, 0, vel.z);
            }
            else if(rb.velocity.y >0){
                rb.velocity = new Vector3(vel.x, vel.y/2, vel.z);
            }
            Invoke(nameof(ResetSalto),enfriamientoSalto);

        }
    }
    void ResetSalto(){
        listoParaSaltar=true;
    }

    void EnElSuelo(Collision other){
        int layer = other.gameObject.layer;
        if(groundMask !=(groundMask | (1<< layer))) return;
        
        for(int i=0; i< other.contactCount; i++){
            Vector3 normal = other.contacts[i].normal;

            if(EsSuelo(normal)){
                tocaSuelo=true;
                cancellSuelo =false;
                vectorNormal=normal;
                CancelInvoke(nameof(PararSuelo));
            }
        }

        float delay = 3f;
        if(!cancellSuelo){
            cancellSuelo=true;
            Invoke(nameof(PararSuelo),Time.deltaTime * delay);
        }

    }
    bool EsSuelo(Vector3 v){
        float angle =Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;//si es más pequeño que el ángulo de rampa es que es suelo
    }
    void PararSuelo(){
        tocaSuelo=false;
    }
}
