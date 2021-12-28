using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionaPassword : MonoBehaviour
{
    // Start is called before the first frame update

    public string contraseña;
    public MovimientoCamara cam;
    public Image imagen;
    public InputField textito;
    public TextMeshProUGUI textoPass;
    public Button boton;
    public Canvas canvas;
    public Canvas mainCanvas;
    public AudioSource buttonSound;

    void Start()
    {
        cam.enabled = false;
        mainCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void guardaPass(){
        buttonSound.Play(0);
        if(textito.text !=""){
            contraseña = textito.text;

            cam.enabled = true;
            canvas.gameObject.SetActive(false);
            mainCanvas.gameObject.SetActive(true);
            Debug.Log(contraseña);
        }
    }
}
