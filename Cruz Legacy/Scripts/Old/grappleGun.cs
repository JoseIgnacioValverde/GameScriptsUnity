using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleGun : MonoBehaviour
{
    public LineRenderer lr;
    public Transform puntaPistola, camara, player;
    private Vector3 grapplePoint;
    public LayerMask grappable;

    private float maxDistance=50f;
    private SpringJoint joint;
    public bool grappled;
    void awake(){
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        drawRope();
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
        RaycastHit hit;
        if(Physics.Raycast(camara.position, camara.forward,out hit,maxDistance)){
            grapplePoint=hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor=false;
            joint.connectedAnchor=grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //distancia mínima y máxima a la que nos podemos quedar con el grapple
            joint.maxDistance = distanceFromPoint *0.75f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //parametros de la cuerda
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale=4.5f;
            grappled=true;
            lr.positionCount=2;
        }
    }
    void StopGrapple(){
        lr.positionCount =0;
        Destroy(joint);
        grappled = false;
    }

    void drawRope(){

        if(!joint) return;
        lr.SetPosition(0, puntaPistola.position);
        lr.SetPosition(1, grapplePoint);
    }
}
