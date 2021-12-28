using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletManager : MonoBehaviour
{
    public LayerMask suelo,target;
    public Rigidbody rigid;
    public float destroyTimer=0.5f;
    public float distanciadeImpacto=5f;
    public Transform puntaflecha;
    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, destroyTimer);
    }

    // Update is called once per frame

}
