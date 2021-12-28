using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public bool inside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other == playerCollider){
            inside=true;
        }
        else{
            inside=false;
        }
        

    }
}
