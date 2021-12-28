using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarCanvas : MonoBehaviour
{
    
    private Canvas canvasActual;
    public Canvas canvasGrande;
    public GameObject generarJuego;

    //Menu de compra
    public GameObject[] menus;
    public GameObject[] unidades;
    public float[] precios;
    public Jugador jugador;
    private bool abierto;
    public Generador generador;
    //Subir velocidad cada minuto
    private float velSpawn = 1;
    private int anunciosSpawn = 0;
    private float tiempoBase = 25;
    private float tiempoDividido = 1;
    private int nivel;

    //sonido
    public AudioSource generateMicrogameSound;

    private float spawnTime;

    private void Awake()
    {
        abierto = false;
    }

    void Start()
    {
        spawnTime = Random.Range(10f, 15f) / velSpawn;
    }
    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0f)
        {
            anunciosSpawn++;
            if (anunciosSpawn >= 5)
            {
                velSpawn++;
                anunciosSpawn = 0;
                if(velSpawn >= 6)
                {
                    tiempoDividido++;
                    velSpawn = 1;
                    nivel++;
                    generador.nivel = nivel;
                }
            }
                
            generateMicrogameSound.Play(0);
            GameObject anuncio =  Instantiate(generarJuego, canvasGrande.gameObject.transform);
            anuncio.GetComponent<generarMicrojuego>().tiempo = tiempoBase / tiempoDividido;
            spawnTime = Random.Range(10f, 15f) / velSpawn;
        }
    }

    public void ToggleCompra()
    {
        if (abierto)
        {
            abierto = false;
            foreach(GameObject menu in menus)
                menu.SetActive(false);
        }

        else
        {
            abierto = true;
            foreach (GameObject menu in menus)
                menu.SetActive(true);
        }
    }

    public void Comprar(int tipo)
    {
        GameObject unit = unidades[tipo];
        float precio = precios[tipo];
        ToggleCompra();
        if(jugador.comprarUnidad(unit, precio))
        {
            Instantiate(unit);
        }
    }

}
