using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public CharacterController controller;
   //private Rigidbody rb;
    public Transform tomaDeTierra;
    //public grappleGun gg;
    //private CapsuleCollider capsule;
    public float distanciaDelSuelo=0.4f;
    public LayerMask groundMask;
    public float speed = 20f;
    public float ropeSpeed=4000f;
    private float basespeed;
    private float clutchspeed;
    private float slideSpeed=10f;
    public float gravedad = -9.8f;
    public float salto= 3f;
    private Vector3 velocity;
    private Vector3 instantVelocity;
    private bool tocaSuelo;
    private bool agachado = false;
    private bool corriendo = false;
    // Start is called before the first frame update
    void Start()
    {
       basespeed=speed;
       clutchspeed=speed/2; 
       //capsule = GetComponent<CapsuleCollider>();
      // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tocaSuelo=Physics.CheckSphere(tomaDeTierra.position, distanciaDelSuelo, groundMask);
        if(tocaSuelo && velocity.y <0){
            velocity.y=-2f;
        }
        float x=Input.GetAxis("Horizontal");
        float z=Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        /*if(gg.grappled){
            ActivaFisicasGrapple();
            rb.AddForce(transform.right * x * ropeSpeed * Time.deltaTime);
            rb.AddForce(transform.forward * z * ropeSpeed * Time.deltaTime);

        }
        else{

            if(tocaSuelo){
                DesactivaFisicasGrapple();
                
            }*/


            controller.Move(move * speed * Time.unscaledDeltaTime);
        //}
        if(tocaSuelo){
            if(Input.GetButtonDown("Sprinting")){
                corriendo=true;
                speed=25;
 
            }
            else if(Input.GetButtonUp("Sprinting")){
                corriendo=false;
                speed=10;
            
            }
        }
        
        
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
        //if(!gg.grappled){
            if(Input.GetButtonDown("Jump")&& tocaSuelo){
                velocity.y = Mathf.Sqrt(salto* -2f *gravedad);
            }
            velocity.y +=gravedad * Time.unscaledDeltaTime;

            controller.Move(velocity*Time.unscaledDeltaTime);
        //}

    }
    void agacharse(){
        speed=clutchspeed;

        transform.localScale= new Vector3(x:1,y:0.5f, z:1);
        //deslizamiento por rampas. Esta roto
        /*if(tocaSuelo){
            RaycastHit hit;//detectamos que haya una rampa
            if(Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0),Vector3.down, out hit, controller.height/2)){
                
                if(hit.normal != Vector3.up){//se que esto es redundante, pero no va si no está puesto por alguna razón
                    slideSpeed=Time.deltaTime*10;
                    velocity=hit.normal + new Vector3(0,0,slideSpeed);//aprovechamos el rayo para cambiar la velocidad
                    controller.Move((velocity+Vector3.down+Vector3.forward)*Time.deltaTime);

                }
                else{
                    slideSpeed-=Time.deltaTime*10;
                    if(slideSpeed>clutchspeed){
                        speed=clutchspeed;
                    }
                }
            }
        }*/

    }
    void levantarse(){
        speed=basespeed;
       transform.localScale= new Vector3(x:1,y:1f, z:1); 

    }
    
    /*void ActivaFisicasGrapple(){

        controller.enabled=false;
        capsule.enabled=true;
        rb.isKinematic=false;


        

    }
    void DesactivaFisicasGrapple(){

        controller.enabled=true;
        capsule.enabled=false;
       rb.isKinematic=true;

    }*/
    
}
