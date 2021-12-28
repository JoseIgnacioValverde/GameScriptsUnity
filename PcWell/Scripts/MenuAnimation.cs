using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuAnimation : MonoBehaviour
{
    public GameObject menuImage;
    public float frec = 2f;
    public bool activateAnimation=true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //AnimationImage();
    }
    void AnimationImage(){
        if(activateAnimation){
            float escala = 2*frec;
            menuImage.transform.localScale=new Vector3(escala, escala, 1);
        }
    }
}
