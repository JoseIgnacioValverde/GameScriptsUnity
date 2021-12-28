using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensibilidad =1000f;
    public Transform body;
    private float rotacionEnX=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //imputs de ratón
        float mouseX = Input.GetAxis("Mouse X")* sensibilidad* Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* sensibilidad* Time.unscaledDeltaTime;

        rotacionEnX-=mouseY;//restamos para que la camara se mueva acorde. con la suma sale camara invertida
        rotacionEnX =Mathf.Clamp(rotacionEnX, -90f, 90f);
        transform.localRotation=Quaternion.Euler(rotacionEnX,0f,0f);
        body.Rotate(Vector3.up*mouseX);

    }
}
