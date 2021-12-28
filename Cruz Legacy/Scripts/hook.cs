using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    public LineRenderer lr;
    public Transform puntaPistola, camara, player;
    public PlayerMovement movimientoJ;
    private Vector3 grapplePoint;
    public LayerMask grappable;
    private float maxDistance=80f;
    public CharacterController cc;
    public CapsuleCollider capc;
    public bool grappled=false;
    public float speed=2f;
    private float gravedadInicial;
    public float distanciaFreno=5f;
    private float distanceFromPoint;
    public AudioSource hookHit;
    void awake(){
        lr = GetComponent<LineRenderer>();
        capc.enabled=false;
        movimientoJ.gravedad=-30f;
        gravedadInicial=movimientoJ.gravedad;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        drawRope();
        Grapple();
        gestorGravedad();
    }
    void Update()
    {

        if(Input.GetButtonDown("Grapple")){

            StartGrapple();
        }
        else if(Input.GetButtonUp("Grapple")){
            StopGrapple();
        }


    }
    void StartGrapple(){
        //if(grappled==false){
            RaycastHit hit;
        if(Physics.Raycast(puntaPistola.position, puntaPistola.forward,out hit,maxDistance,grappable)){
            if(speed==30f){
                
            }
            hookHit.Play();
            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            grapplePoint=hit.point;
            //Grapple();
            drawRope();
            /*capc.enabled=true;
            cc.enabled=false;*/
           

            grappled=true;
            lr.positionCount=2;
        }
        //}
        
    }
    void StopGrapple(){
        lr.positionCount =0;
        /*capc.enabled=false;
        cc.enabled=true;*/
        grappled = false;
        
    }

    void Grapple(){
        if(grappled==true){
            
            Vector3 velocity=new Vector3(0,0,0);
            //Vector3 position= Vector3.Lerp(player.position,grapplePoint,speed);
            Vector3 position = Vector3.SmoothDamp(player.position,grapplePoint,ref velocity, 2f)-player.position; 
            //position=absolutos(position); 
            position = position + player.forward;
            cc.Move(position);
            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            Debug.Log("position: "+position);
            if(distanceFromPoint<=distanciaFreno){
                grappled=false;

                StopGrapple();

            }
            
        }

    }

    void drawRope(){
        if(!grappled) return;
        lr.SetPosition(0, puntaPistola.position);
        lr.SetPosition(1, grapplePoint);
    }
    void gestorGravedad(){
        if(grappled){
            movimientoJ.gravedad=0;
        }
        else{
            movimientoJ.gravedad=-30f;
        }
    }
    Vector3 absolutos(Vector3 vector){
        Vector3 vectorRes=vector;
        if(vectorRes.x<0f){
            vectorRes.x=vectorRes.x*-1;
        }
        if(vectorRes.y<0f){
            vectorRes.y=vectorRes.y*-1;
        }
        if(vectorRes.z<0f){
            vectorRes.z=vectorRes.z*-1;
        }
        return vectorRes;
    }
}
