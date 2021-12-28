using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    
    private Quaternion startPos;
    public Transform fork;
    void Start()
    {
        startPos=fork.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        fork.Rotate(new Vector3(0f,0f,1f));
    }
    void fixedUpdate(){
        
    }
}
